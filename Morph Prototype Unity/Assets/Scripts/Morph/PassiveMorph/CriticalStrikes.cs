using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalStrikes : PassiveMorph
{
    private DamageHandler damageHandler;

    [SerializeField] private float criticalStrikeChance;
    [SerializeField] private bool hiddenAttack = true;

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
        if (damageTakenSummary.AcidDamage > 0)
        {
            //damageTakenSummary.DamageTaker.ApplyDamage(new PerceptionDamageData(damageTakenSummary.AcidDamage * perceptionDamageFraction), damageHandler);
        }
    }

    private void OnDamageAboutToBeDealt(ref IDamageType damageType)
    {
        if (damageType is IPhysicalDamage physicalDamage)
        {
            //physicalDamage. *= 2;
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
            damageHandler.DamageAboutToBeDealt += OnDamageAboutToBeDealt;
            if (hiddenAttack)
            {

            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageAboutToBeDealt -= OnDamageAboutToBeDealt;
            if (hiddenAttack)
            {

            }

        }

        damageHandler = null;
    }
}
