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

    //this timer starts every time stamina is spent, during this timer stamina wont regenerate
    [SerializeField] private Timer staminaRegenTimer;
    float staminaRegenTimerDuration = 1f;
    public bool staminaRegenOnCooldown;
    float staminaRegen = 5;
    float globalStaminaRegenFactor = 50;

    Stats stats;

    [SerializeField] private Image staminaBar;
    private Coroutine hideStaminaBarAfterTime;

    // Start is called before the first frame update
    void Start()
    { 
        staminaRegenOnCooldown = false;
        stats = GetComponent<Stats>();
        baseMaxStamina = stats ? stats.MaxStamina : 100;
        staminaRegenTimer = new Timer(staminaRegenTimerDuration, false);

        Invoke("SetMaxStamina", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaRegenOnCooldown) 
        {
            staminaRegenTimer.Update(Time.deltaTime);
        }  

        if (staminaRegenTimer.JustCompleted) 
        {
            staminaRegenOnCooldown = false;
        }

        if (!staminaRegenOnCooldown) 
        {
            StaminaRegen();
        }
    }

    void StaminaRegen() 
    {
        float staminaToAdd = staminaRegen * (1 + bonusStaminaRegen + energyAsPercentage) / globalStaminaRegenFactor;

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

        T_UpdateStaminaBar();
    }

    

    public float StaminaAsPercentage()
    {
        float staminaAsPercetage = currentStamina / totalMaxStamina;


        return staminaAsPercetage;
    }

    public void SubtractStamina(float amount)
    {
        //if timer is still counting down, spend the stamina and restart the timer from the beginning 
        if (staminaRegenOnCooldown)
        {
            staminaRegenTimer = new Timer(staminaRegenTimerDuration, false);
        }

        currentStamina = Mathf.Max(0, currentStamina - amount);
        staminaRegenOnCooldown = true;
        T_UpdateStaminaBar();

        
    }

    private void T_SetUpStaminabar()
    {
        //staminaBar = GameObject.Find("UI").transform.Find("Gameplay").transform.Find("StatusBar").transform.Find("StaminaBar").GetComponent<Image>();
        staminaBar.gameObject.SetActive(false);
    }

    private void T_UpdateStaminaBar()
    {
        staminaBar.gameObject.SetActive(true);
        staminaBar.fillAmount = currentStamina / totalMaxStamina;
        if (hideStaminaBarAfterTime != null) StopCoroutine(hideStaminaBarAfterTime);
        hideStaminaBarAfterTime = StartCoroutine(HideStaminaBarAfterTimeCoroutine(2));
    }

    private IEnumerator HideStaminaBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        staminaBar.gameObject.SetActive(false);
    }
}
