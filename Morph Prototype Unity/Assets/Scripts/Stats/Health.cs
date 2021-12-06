using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    private Stats stats;

    public float CurrentHealth=> health;
    public float CurrentHealthAsPercentage => health / stats.MaxHealth;
    public event Action Died;
    public event Action<float> HealthChanged;

    // TODO - reimplement health bar
    [SerializeField] private Image healthBar;
    private Coroutine hideHealthBarAfterTime;
    
    // Start is called before the first frame update
    void Awake()
    {
        //get stats component
        stats = GetComponent<Stats>();
        T_SetUpHealthbar();
        
        health = stats ? stats.MaxHealth : 100;

    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void SubtractHP(float amount)
    {
        health -= amount;
        health = Mathf.Max(0, health);
        
        OnHealthChanged();
        
        if (health <= 0)
        {
            Die();
        }
        
        
    }

    public void AddHP(float amount)
    {
        
    }

    private void OnHealthChanged()
    {
        HealthChanged?.Invoke(health);
        
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
        return health - damage <= 0;
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
        healthBar.fillAmount = CurrentHealth / stats.MaxHealth;
        if(hideHealthBarAfterTime != null) StopCoroutine(hideHealthBarAfterTime);
        hideHealthBarAfterTime = StartCoroutine(HideHealthBarAfterTimeCoroutine(2));
    }

    private IEnumerator HideHealthBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        healthBar.gameObject.SetActive(false);
    }
}
