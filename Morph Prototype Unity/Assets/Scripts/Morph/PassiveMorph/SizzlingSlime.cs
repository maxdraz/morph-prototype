using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizzlingSlime : PassiveMorph
{
    private DamageHandler damageHandler;

    [SerializeField] private float acidDOTModifier;
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
            damageTakenSummary.DamageTaker.ApplyDamage(new PerceptionDamageData(damageTakenSummary.AcidDamage/10), damageHandler);
        }
    }

    private void OnAcidDebuffDealt(ref IDamageType damageType, DamageHandler damageTaker)
    {
        if (damageType is IAcidDamage acidDamage)
        {
            DoubleAcidDot(ref acidDamage);
        }
    }

    private void DoubleAcidDot(ref IAcidDamage acidDamage)
    {

        acidDamage.AcidDotModifier += acidDOTModifier;
        print("buffed acid Dot: " + acidDamage.AcidDotModifier);
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
            damageHandler.DebuffAboutToBeDealtPreModifier -= OnAcidDebuffDealt;
            if (unlockBlindingVapour)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DebuffAboutToBeDealtPreModifier -= OnAcidDebuffDealt;
            if (unlockBlindingVapour)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            }

        }

        damageHandler = null;
    }
}
