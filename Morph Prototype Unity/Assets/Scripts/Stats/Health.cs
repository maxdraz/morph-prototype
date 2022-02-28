using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float baseMaxHealth;
    [SerializeField] private float maxHealth;
    private Stats stats;
    public float maxHealthBonus;
    public float healingPercentageBonus = 0f;

    public float currentHealth;
    public float CurrentHealthAsPercentage => baseMaxHealth / maxHealth;
    public event Action Died;
    public event Action<float> HealthChanged;

    float particleThreshold;
    [SerializeField] private GameObject healthGainParticles;

    // TODO - reimplement health bar
    [SerializeField] private Image healthBar;
    private Coroutine hideHealthBarAfterTime;



    // Start is called before the first frame update
    void Awake()
    {
        //get stats component
        stats = GetComponent<Stats>();


        baseMaxHealth = stats ? stats.MaxHealth : 100;

        Invoke("SetMaxHP", .5f);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void SetMaxHP() 
    {
        maxHealth = baseMaxHealth * (1 + maxHealthBonus);
        currentHealth = maxHealth;

        T_SetUpHealthbar();
    }

    public void SubtractHP(float amount)
    {
        
        currentHealth = Mathf.Max(0, currentHealth - amount);
        
        OnHealthChanged();
        
        if (currentHealth == 0)
        {
            Die();
        }     
    }

    public void AddHP(float amount)
    {
        float amountToHeal = amount* (1 + healingPercentageBonus);
        currentHealth = Mathf.Min(currentHealth + amountToHeal, maxHealth);

        if (amount > particleThreshold)
        {
            GameObject particles = ObjectPooler.Instance.GetOrCreatePooledObject(healthGainParticles);
            particles.transform.position = transform.position;
            particles.transform.parent = transform;
        }

        OnHealthChanged();
    }

    

    public void AddPercentHP(float amount)
    {
        float healthToHeal = maxHealth * amount;
        AddHP(healthToHeal);
    }

    public IEnumerator HealOverTime(float amount, int duration) 
    {
        float amountToHealPerSecond = amount / duration;
        int secondsRemaining = duration;
        yield return new WaitForSeconds(1);
        secondsRemaining--;
        AddHP(amountToHealPerSecond);

        if (secondsRemaining > 0) 
        {
            StartCoroutine(HealOverTime (amount-amountToHealPerSecond, secondsRemaining));
        }
        yield return null;
    }

    private void OnHealthChanged()
    {
        HealthChanged?.Invoke(baseMaxHealth);
        
        T_UpdateHealthBar();
    }

    

    private void Die()
    {
        // temp
        GetComponentInParent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<DebuffHandler>().StopAllDebuffs();
        Died?.Invoke();
    }

    public bool WillDieFromThisDamage(float damage)
    {
        return baseMaxHealth - damage <= 0;
    }

    private void T_SetUpHealthbar()
    {
        healthBar = transform.Find("HealthBarCanvas").Find("HealthBar").GetComponent<Image>();
        healthBar.gameObject.SetActive(false);
    }

    private void T_UpdateHealthBar()
    {
        // health bar
        healthBar.gameObject.SetActive(true);
        healthBar.fillAmount = currentHealth / stats.MaxHealth;
        if(hideHealthBarAfterTime != null) StopCoroutine(hideHealthBarAfterTime);
        hideHealthBarAfterTime = StartCoroutine(HideHealthBarAfterTimeCoroutine(2));
    }

    private IEnumerator HideHealthBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        healthBar.gameObject.SetActive(false);
    }
}
