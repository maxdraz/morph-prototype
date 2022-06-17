using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : PassiveMorph
{
    [SerializeField] private float staminaRegenBonus = .1f;
    [SerializeField] private float energyRegenBonus = .15f;

    [SerializeField] private bool unlockRecuperate;
    [SerializeField] private float recuperateTimerDuration = 1;
    [SerializeField] private Timer recuperateTimer;
    public bool recuperating;

    private Stamina stamina;
    private Energy energy;
    public Velocity velocity;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        velocity = GetComponentInParent<Velocity>();
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ChangeStaminaEnergyRegenStat(staminaRegenBonus, energyRegenBonus);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ChangeStaminaEnergyRegenStat(-staminaRegenBonus, -energyRegenBonus);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Recuperate")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockRecuperate = true;
        }
    }

    // implement
    private void ChangeStaminaEnergyRegenStat(float staminaRegen, float energyRegen)
    {
        stamina.bonusStaminaRegen += staminaRegen;
        energy.bonusEnergyRegen += energyRegen;
    }

    private void Update()
    {
        if (unlockRecuperate) 
        {
            if (velocity.CurrentVelocity.magnitude == 0) 
            {
                recuperateTimer = new Timer(recuperateTimerDuration, false);
                recuperateTimer.Update(Time.deltaTime);

                if (recuperateTimer.JustCompleted)
                {
                    recuperating = true;
                    ChangeStaminaEnergyRegenStat(staminaRegenBonus, energyRegenBonus);
                }
            }

            else
            {
                if (recuperating == true) 
                {
                    recuperating = false;
                    ChangeStaminaEnergyRegenStat(-staminaRegenBonus, -energyRegenBonus);
                }
            }
        }
    }
}
