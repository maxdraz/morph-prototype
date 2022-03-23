using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizzlingSlime : PassiveMorph
{
    static int chemicalDamagePrerequisit = 35;

    private DamageHandler damageHandler;

    [SerializeField] private float perceptionDamageFraction;
    [SerializeField] private int chemicalDamageStatBonus = 5;
    [SerializeField] private bool unlockBlindingVapour = true;

    Stats stats;

    //static Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    private void ChangeChemicalDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("chemicalDamage", amountToAdd); 
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
