using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float baseMaxEnergy;
    public float maxEnergyBonus;
    public float bonusEnergyRegen;
    [SerializeField] private float totalMaxEnergy;
    public float currentEnergy;

    //this timer starts every time energy is spent, during this timer energy wont regenerate
    [SerializeField] private Timer energyRegenTimer;
    float energyRegenTimerDuration = 1f;
    bool canRegenEnergy;
    float energyRegen = 5;
    float globalEnergyRegenFactor = 100;

    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxEnergy();   
    }

    // Update is called once per frame
    void Update()
    {
        if (canRegenEnergy)
        {
            EnergyRegen();
        }

        else
        {
            energyRegenTimer.Update(Time.deltaTime);

            if (energyRegenTimer.JustCompleted)
            {
                canRegenEnergy = true;
            }
        }
    }

    void EnergyRegen()
    {
        currentEnergy += (energyRegen * (1 + bonusEnergyRegen)) / globalEnergyRegenFactor;
    }

    private void SetMaxEnergy()
    {
        totalMaxEnergy = baseMaxEnergy * (1 + maxEnergyBonus);
        currentEnergy = totalMaxEnergy;
    }

    public void AddEnergy(float amount)
    {
        totalMaxEnergy = Mathf.Min(currentEnergy + amount, totalMaxEnergy);

    }

    public void RefundEnergy(float amountSpent, float amountToRefund)
    {
        float energyToRefund = amountSpent * amountToRefund;


        AddEnergy(energyToRefund);

    }

    public float EnergyAsPercentage() 
    {
        float energyAsPercetage = currentEnergy / totalMaxEnergy; 


        return energyAsPercetage;
    }

    public void SubtractEnergy(float amount)
    {


        totalMaxEnergy = Mathf.Max(0, totalMaxEnergy - amount);
        canRegenEnergy = false;

        //if timer is still counting down
        //if ()
        //{
        //restart the timer from the beginning
        //}
        //else
        //{
        //   canRegenEnergy = false;
        //}
    }
}
