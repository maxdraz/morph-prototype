using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoxingSchool : PassiveMorph
{

    private DamageHandler damageHandler;
    [SerializeField] private float meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockNinjaTraining = true;
    [SerializeField] private float shadowBoxingbonusDamageMultiplier;
    Stats stats;

    [SerializeField] private float ninjaTrainingCritChanceToStealthMultiplicationFactor;
    [SerializeField] private float ninjaTrainingAgilityToCritChanceMultiplicationFactor;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
    }

    // implement
    private void ChangeMeleeDamageStat(float amountToAdd)
    {

    }

    private void OnDamageAboutToBeDealt(in DamageTakenSummary damageTakenSummary)
    {
        //float shadowBoxingSchoolBonusDamage = Mathf.Sqrt(stats.stealth) * shadowBoxingbonusDamageMultiplier;

        //add shadowBoxingSchoolBonusDamage to the bonus flat damage value in the physical damage formula

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
            damageHandler.DamageHasBeenDealt += OnDamageAboutToBeDealt;

        }
    }

    private void NinjaTraining() 
    {
        //float bonusStealth = stats.stealth * (critChance * ninjaTrainingCritChanceToStealthMultiplicationFactor);
        //Send this value out to modify current stealth value

        //float bonusCritChance = Mathf.Sqrt(stats.agility) * ninjaTrainingAgilityToCritChanceMultiplicationFactor;
        //Send this value out to modify current crit chance
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageAboutToBeDealt;

        }

        damageHandler = null;
    }
}
