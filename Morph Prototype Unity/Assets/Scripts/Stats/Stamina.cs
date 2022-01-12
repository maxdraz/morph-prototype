using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField]private float baseMaxStamina;
    public float maxStaminaBonus;
    [SerializeField] private float totalMaxStamina;
    [SerializeField] private float currentStamina;

    //this timer starts every time stamina is spent, during this timer stamina wont regenerate
    [SerializeField] private Timer staminaRegenTimer;
    float staminaRegenTimerDuration = 1f;
    bool canRegenStamina;
    float staminaRegen = 5;
    float globalStaminaRegenFactor = 100;
    float bonusStaminaRegen;

    Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        baseMaxStamina = stats ? stats.MaxStamina : 100;
        SetMaxStamina();
    }

    // Update is called once per frame
    void Update()
    {
        if (canRegenStamina) 
        {
            StaminaRegen();
        }

        else  
        {
            staminaRegenTimer.Update(Time.deltaTime);

            if (staminaRegenTimer.JustCompleted) 
            {
                canRegenStamina = true;
            }
        }
    }

    void StaminaRegen() 
    {
        currentStamina += (staminaRegen * (1 + bonusStaminaRegen)) / globalStaminaRegenFactor;
    }
  
    private void SetMaxStamina()
    {
        totalMaxStamina = baseMaxStamina * (1 + maxStaminaBonus);
        currentStamina = totalMaxStamina;
    }

    public void AddStamina(float amount)
    {
        totalMaxStamina = Mathf.Min(currentStamina + amount, totalMaxStamina);

    }

    public void SubtractStamina(float amount)
    {
        totalMaxStamina = Mathf.Max(0, totalMaxStamina - amount);
        canRegenStamina = false;

        //if timer is still couting down
        //if ()
        //{
        //restart the timer from the beginning
        //}
        //else
        //{
        //   canRegenStamina = false;
        //}
    }
}
