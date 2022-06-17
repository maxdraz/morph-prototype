using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealer : PassiveMorph
{
    [SerializeField] private float lifeStealFraction;
    [SerializeField] private bool unlockFierceHunger;

    [SerializeField] private bool unlockBloodscent;

    public void UnlockSecondary(string name)
    {
        if (name == "FierceHunger")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockFierceHunger = true;
        }

        if (name == "Bloodscent")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockBloodscent = true;
        }
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0)
        {
            if (damageHandler.Health.CurrentHealthAsPercentage <= .3f && unlockFierceHunger)
            {
                damageTakenSummary.DamageTaker.ApplyDamage(new LifeStealData(damageTakenSummary.PhysicalDamage * lifeStealFraction * 2), damageHandler);
            }
            else 
            {
                damageTakenSummary.DamageTaker.ApplyDamage(new LifeStealData(damageTakenSummary.PhysicalDamage * lifeStealFraction), damageHandler);
            }
        }
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
