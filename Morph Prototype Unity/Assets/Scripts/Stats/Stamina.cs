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
    bool staminaRegenOnCooldown;
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
        SetMaxStamina();
        staminaRegenTimer = new Timer(staminaRegenTimerDuration, false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!staminaRegenOnCooldown) 
        {
            if (currentStamina < totalMaxStamina) 
            {
                StaminaRegen();
            }
        }

        else  
        {
            staminaRegenTimer.Update(Time.deltaTime);

            if (staminaRegenTimer.JustCompleted) 
            {
                staminaRegenOnCooldown = false;
            }
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
        totalMaxStamina = Mathf.Min(currentStamina + amount, totalMaxStamina);

        T_UpdateStaminaBar();
    }

    public void RefundStamina(float amountSpent, float amountToRefund)
    {
        float staminaToRefund = amountSpent * amountToRefund;
         

        AddStamina(staminaToRefund);

    }

    public float StaminaAsPercentage()
    {
        float staminaAsPercetage = currentStamina / totalMaxStamina;


        return staminaAsPercetage;
    }

    public void SubtractStamina(float amount)
    {
        

        totalMaxStamina = Mathf.Max(0, totalMaxStamina - amount);
        staminaRegenOnCooldown = true;
        T_UpdateStaminaBar();

        //if timer is still counting down, spend the stamina and restart the timer from the beginning 
        if (staminaRegenTimer.CurrentTime < staminaRegenTimer.Duration)
        {
            staminaRegenTimer = new Timer(staminaRegenTimerDuration, false);
        }
    }

    private void T_SetUpStaminabar()
    {
        staminaBar = transform.Find("UI").Find("Gameplay").Find("StatusBar").Find("StaminaBar").GetComponent<Image>();
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
