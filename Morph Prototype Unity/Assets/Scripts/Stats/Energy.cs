using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float baseMaxEnergy;
    public float bonusMaxEnergy;
    public float bonusEnergyRegen;
    [SerializeField] private float totalMaxEnergy;
    public float currentEnergy;
    float energyAsPercentage;

    //this timer starts every time energy is spent, during this timer energy wont regenerate
    [SerializeField] private Timer energyRegenTimer;
    float energyRegenTimerDuration = 1f;
    bool energyRegenOnCooldown;
    float energyRegen = 5;
    float globalEnergyRegenFactor = 100;

    Stamina stamina;
    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stamina = GetComponent<Stamina>();
        stats = GetComponent<Stats>();
        SetMaxEnergy();
        energyRegenTimer = new Timer(energyRegenTimerDuration, false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!energyRegenOnCooldown)
        {
            EnergyRegen();
        }

        else
        {
            energyRegenTimer.Update(Time.deltaTime);

            if (energyRegenTimer.JustCompleted)
            {
                energyRegenOnCooldown = false;
            }
        }

        EnergyAsPercentage();
        stamina.energyAsPercentage = energyAsPercentage;
    }

    void EnergyRegen()
    {
        currentEnergy += (energyRegen * (1 + bonusEnergyRegen)) / globalEnergyRegenFactor;
    }

    public void SetMaxEnergy()
    {
        totalMaxEnergy = baseMaxEnergy * (1 + bonusMaxEnergy);
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
        energyAsPercentage = currentEnergy / totalMaxEnergy;
        return energyAsPercentage;
    }

    public void SubtractEnergy(float amount)
    {


        totalMaxEnergy = Mathf.Max(0, totalMaxEnergy - amount);
        energyRegenOnCooldown = true;

        //if timer is still counting down, spend the energy and restart he timer from the beginning 
        if (energyRegenTimer.CurrentTime < energyRegenTimer.Duration)
        {
            energyRegenTimer = new Timer(energyRegenTimerDuration, false);
        }
    }
}
