using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : PassiveMorph
{
    [SerializeField] private bool unlockSpiritLeech = false;

    [SerializeField] private float lifeStealFraction = .2f;
    [SerializeField] private float energyStealFraction = .4f;

    public void UnlockSecondary(string name)
    {
        if (name == "SpiritLeech")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockSpiritLeech = true;
        }
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0) 
        {
            damageTakenSummary.DamageTaker.ApplyDamage(new LifeStealData(damageTakenSummary.PhysicalDamage * lifeStealFraction), damageHandler);

            if (unlockSpiritLeech) 
            {
                damageTakenSummary.DamageTaker.ApplyDamage(new EnergyStealData(damageTakenSummary.PhysicalDamage * lifeStealFraction * energyStealFraction), damageHandler);
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
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
        }
    }
}
