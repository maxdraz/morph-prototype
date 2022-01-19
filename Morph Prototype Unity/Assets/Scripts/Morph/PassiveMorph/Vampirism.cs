using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private bool unlockSpiritLeech = true;

    [SerializeField] private float lifeStealFraction = .2f;
    [SerializeField] private float energyStealFraction = .4f;
    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
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

            private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {

            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {

            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;

        }

        damageHandler = null;
    }
}
