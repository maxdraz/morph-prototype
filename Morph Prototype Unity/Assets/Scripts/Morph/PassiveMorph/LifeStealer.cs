using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealer : PassiveMorph
{
    private DamageHandler damageHandler;

    [SerializeField] private float lifeStealFraction;
    [SerializeField] private bool fierceHunger = true;

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0)
        {
            if (damageHandler.Health.CurrentHealthAsPercentage <= .3f && fierceHunger)
            {
                damageTakenSummary.DamageTaker.ApplyDamage(new LifeStealData(damageTakenSummary.PhysicalDamage * lifeStealFraction * 2), damageHandler);
            }
            else 
            {
                damageTakenSummary.DamageTaker.ApplyDamage(new LifeStealData(damageTakenSummary.PhysicalDamage * lifeStealFraction), damageHandler);
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
