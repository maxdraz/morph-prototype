using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitriolicVitality : PassiveMorph
{
    static int chemicalDamagePrerequisit = 30;
    static int toughnessPrerequisit = 30;
    static int fortitudePrerequisit = 30;

    [SerializeField] private float healingPercentageBonus;
    Timer bonusHealingTimer;
    bool bonusHealingTimerCountingDown;
    [SerializeField] private float bonusHealingTimerDuration;

    [SerializeField] private bool unlockVenomousVigor;
    [SerializeField] private int venomousVigorHealingDuration;

    [SerializeField] private bool unlockToxicFocus;
    [SerializeField] private float toxicFocusBonusPoisonDamage;
    
    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);
    }
    
    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "VenomousVigor")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockVenomousVigor = true;
        }

        if (name == "ToxicFocus")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockToxicFocus = true;
        }
    }

    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    void ModifyStats(bool AddToStat)
    {
        if (stats != null)
        {
            if (statsToModify.Length > 0)
            {
                for (int i = 0; i <= statsToModify.Length - 1; i++)
                {
                    if (AddToStat)
                    {
                        Debug.Log(GetType().Name + " is adding" + statsToModify[i].value + " to " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), statsToModify[i].value);
                    }
                    else
                    {
                        Debug.Log(GetType().Name + " is removing" + statsToModify[i].value + " from " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), -statsToModify[i].value);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bonusHealingTimerCountingDown) 
        {
            bonusHealingTimer.Update(Time.deltaTime) ;
        }

        if (bonusHealingTimer.JustCompleted) 
        {
            bonusHealingTimerCountingDown = false;
            RemoveHealingBonus();
        }
    }

    void AddHealingBonus() 
    {
        damageHandler.Health.healingPercentageBonus += healingPercentageBonus;
    }

    void RemoveHealingBonus()
    {
        damageHandler.Health.healingPercentageBonus -= healingPercentageBonus;
    }

    private void OnDamageAboutToBeDealt(ref IDamageType damageType) 
    {
        //if the attack was a heavy attack from a weapon morph, add poison damage = 30% of the physical damage before resistances
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PoisonDamage > 0)
        {
            //This timer should restart every time a poison damage tick has been dealt to the target
            bonusHealingTimer = new Timer(bonusHealingTimerDuration, false);
            bonusHealingTimerCountingDown = true;
            AddHealingBonus();

            if (unlockVenomousVigor) 
            {
                //This hp should be added when a target has had poison damage added to their stack
                damageHandler.Health.HealOverTime(damageTakenSummary.PoisonDamage / 2f, venomousVigorHealingDuration);
            }
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;

            if (unlockToxicFocus)
            {
                damageHandler.DamageAboutToBeDealt += OnDamageAboutToBeDealt;
            }
        }
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;

            if (unlockToxicFocus)
            {
                damageHandler.DamageAboutToBeDealt -= OnDamageAboutToBeDealt;
            }
        }
    }
}