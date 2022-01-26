using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoxingSchool : PassiveMorph
{

    private DamageHandler damageHandler;
    [SerializeField] private int stealthStatBonus = 50;
    [SerializeField] private bool unlockNinjaTraining = true;
    [SerializeField] private float shadowBoxingbonusDamageMultiplier = 5;
    
    Stats stats;

    [SerializeField] private float ninjaTrainingCritChanceToStealthMultiplicationFactor;
    [SerializeField] private float ninjaTrainingAgilityToCritChanceMultiplicationFactor;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        ChangeStealthStat(stealthStatBonus);

        StartCoroutine(AssignDamageHandlerCoroutine());

        if (unlockNinjaTraining)
        {
            NinjaTraining();
        }

        float shadowBoxingSchoolBonusDamage = Mathf.Sqrt(stats.totalStealth) * shadowBoxingbonusDamageMultiplier;
        
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeStealthStat(-stealthStatBonus);

        
    }

    // implement
    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
    }

    private void OnDamageAboutToBeDealt(in DamageTakenSummary damageTakenSummary)
    {
        

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
        //float bonusCritChance = Mathf.Sqrt(stats.agility) * ninjaTrainingAgilityToCritChanceMultiplicationFactor;
        //Send this value out to modify current crit chance

        //float bonusStealth = stats.stealth * (critChance * ninjaTrainingCritChanceToStealthMultiplicationFactor);
        //Send this value out to modify current stealth value
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
