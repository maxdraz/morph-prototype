using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdged : PassiveMorph
{
    static int meleeDamagePrerequisite = 45;
    static int agilityPrerequisite = 30;

    [SerializeField] private float doubleEdgedHPCost;
    [SerializeField] private bool unlockBloodGuzzler;
    private bool bloodGuzzlerReady;
    private float damageBoost;
    private float damageBoostFactor = 2;

    protected override void OnEquip()
    {
        base.OnEquip();
        
        bloodGuzzlerReady = true;
        ModifyStats(true);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        bloodGuzzlerReady = false;
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "BloodGuzzler")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockBloodGuzzler = true;
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

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0)
        {
            ReduceHPToBoostDamage();
            damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(damageTakenSummary.PhysicalDamage* damageBoost), damageHandler);
        }
        if (unlockBloodGuzzler) 
        {
            if (damageTakenSummary.isMortalBlow && bloodGuzzlerReady) 
            {
                GetComponent<CombatResources>().currentStaminaPoints += damageTakenSummary.PhysicalDamage;
                damageHandler.Health.HealOverTime(damageTakenSummary.PhysicalDamage, 5);
                bloodGuzzlerReady = false;
                StartCoroutine("BloodGuzzlerCooldown");
            }
        }
    }

    private IEnumerator BloodGuzzlerCooldown() 
    {
        yield return new WaitForSeconds(20);
        bloodGuzzlerReady = true;
    }

    private void ReduceHPToBoostDamage() 
    {
        float hpToReduce = damageHandler.Health.currentHealth * doubleEdgedHPCost;
        damageBoost = stats.MaxHealth / hpToReduce;
        damageHandler.Health.SubtractHP(hpToReduce);
        damageBoost *= damageBoostFactor;
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
        }
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
        }
    }
}
