using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stamina : MonoBehaviour
{
    [SerializeField]private float baseMaxStamina;
    public float maxStaminaBonus;
    public float bonusStaminaRegen;
    [SerializeField] private float totalMaxStamina;
    public float currentStamina;
    public float energyAsPercentage;
    public float CurrentStaminaAsPercentage => currentStamina / totalMaxStamina;

    float staminaRegenTimerDuration = 1f;
    public bool staminaRegenOnCooldown;
    float staminaRegen = 5;
    float globalStaminaRegenFactor = 50;

    Stats stats;

    float particleThreshold = 10;
    [SerializeField] private GameObject staminaGainParticles;

    [SerializeField] private Image staminaBar;
    private Coroutine hideStaminaBarAfterTime;

    // Start is called before the first frame update
    void Start()
    { 
        staminaRegenOnCooldown = false;
        stats = GetComponent<Stats>();
        baseMaxStamina = stats ? stats.MaxStamina : 100;

        Invoke("SetMaxStamina", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStamina < totalMaxStamina)
        {
            if (!staminaRegenOnCooldown)
            {
                StaminaRegen();
            }
        }
    }

    void StaminaRegen() 
    {
        float staminaToAdd = staminaRegen * (1 + bonusStaminaRegen + energyAsPercentage) / globalStaminaRegenFactor;
        //T_UpdateStaminaBar();
        AddStamina(staminaToAdd);
    }
  
    public void SetMaxStamina()
    {
        totalMaxStamina = baseMaxStamina * (1 + maxStaminaBonus);
        currentStamina = totalMaxStamina;

        T_SetUpStaminabar();
    }

    public void AddStamina(float amount)
    {
        currentStamina = Mathf.Min(currentStamina + amount, totalMaxStamina);

        if (amount > particleThreshold)
        {
            GameObject particles = ObjectPooler.Instance.GetOrCreatePooledObject(staminaGainParticles);
            particles.transform.position = transform.position;
            particles.transform.parent = transform;
        }

        T_UpdateStaminaBar();
    }

    

   // public float CurrentStaminaAsPercentage()
   // {
   //     float staminaAsPercetage = currentStamina / totalMaxStamina;
   //
   //
   //     return staminaAsPercetage;
   // }

    

    public void SubtractStamina(float amount)
    {
        currentStamina = Mathf.Max(0, currentStamina - amount);
        T_UpdateStaminaBar();

        if (staminaRegenOnCooldown)
        {
            StopCoroutine("RegenTimer");
            StartCoroutine("RegenTimer");
        }
        else
        {
            StartCoroutine("RegenTimer");
        }
    }

    IEnumerator RegenTimer()
    {
        staminaRegenOnCooldown = true;

        yield return new WaitForSeconds(staminaRegenTimerDuration);

        staminaRegenOnCooldown = false;

    }


    private void T_SetUpStaminabar()
    {
        //staminaBar = GameObject.Find("UI").transform.Find("Gameplay").transform.Find("StatusBar").transform.Find("StaminaBar").GetComponent<Image>();
        staminaBar.gameObject.SetActive(false);
    }

    private void T_UpdateStaminaBar()
    {
        staminaBar.GetComponent<Image>().color = new Color(255, 255, 0, 255);
        staminaBar.fillAmount = currentStamina / totalMaxStamina;
        if (hideStaminaBarAfterTime != null) StopCoroutine(hideStaminaBarAfterTime);
        hideStaminaBarAfterTime = StartCoroutine(HideStaminaBarAfterTimeCoroutine(2));
    }

    private IEnumerator HideStaminaBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        staminaBar.GetComponent<Image>().color = new Color(255, 255, 0, 0);
    }
}
