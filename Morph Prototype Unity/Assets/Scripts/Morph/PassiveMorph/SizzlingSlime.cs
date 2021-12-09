using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizzlingSlime : PassiveMorph
{
    private DamageHandler damageHandler;

    [SerializeField] private float perceptionDamageFraction;
    [SerializeField] private float chemicalDamageStatBonus = 5;
    [SerializeField] private bool unlockBlindingVapour = true;

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    private void ChangeChemicalDamageStat(float amountToAdd)
    {

    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.AcidDamage > 0)
        {
            damageTakenSummary.DamageTaker.ApplyDamage(new PerceptionDamageData(damageTakenSummary.AcidDamage * perceptionDamageFraction), damageHandler);
        }
    }

    private void OnAcidDamageAboutToBeDealt(ref IDamageType damageType)
    {
        if (damageType is IAcidTickDamage acidTickDamage)
        {
            acidTickDamage.AcidDamage *= 2;
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
            damageHandler.DamageAboutToBeDealt += OnAcidDamageAboutToBeDealt;
            if (unlockBlindingVapour)
            {
                damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageAboutToBeDealt -= OnAcidDamageAboutToBeDealt;
            if (unlockBlindingVapour)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            }

        }

        damageHandler = null;
    }
}
