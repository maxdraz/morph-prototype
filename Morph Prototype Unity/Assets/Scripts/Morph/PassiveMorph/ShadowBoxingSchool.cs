using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoxingSchool : PassiveMorph
{
    static int stealthPrerequisit = 150;
    [SerializeField] private ShadowBoxingSchoolPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private int stealthStatBonus = 50;
    [SerializeField] private bool unlockNinjaTraining = true;
    [SerializeField] private float shadowBoxingBonusDamageMultiplier = 5;
    float shadowBoxingSchoolBonusDamage;
    Stats stats;

    [SerializeField] private float ninjaTrainingCritChanceToStealthMultiplicationFactor;
    [SerializeField] private float ninjaTrainingAgilityToCritChanceMultiplicationFactor;

    //static Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        ChangeStealthStat(stealthStatBonus);

        StartCoroutine(AssignDamageHandlerCoroutine());

        if (unlockNinjaTraining)
        {
            NinjaTraining();
        }

        shadowBoxingSchoolBonusDamage = Mathf.Sqrt(stats.totalStealth) * shadowBoxingBonusDamageMultiplier;
        
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

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
         if (damageTakenSummary.isMeleeAttack)
         {
             damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(shadowBoxingSchoolBonusDamage), damageHandler);
         }
    }
    private void NinjaTraining()
    {
        float bonusCritChance = Mathf.Sqrt(stats.totalAgility) * ninjaTrainingAgilityToCritChanceMultiplicationFactor;
        stats.globalCritChance += bonusCritChance;
        Debug.Log("NinjaTraining added: " + bonusCritChance + " globalCritChance");

        int bonusStealth = (int)(Mathf.Sqrt(stats.globalCritChance) * ninjaTrainingCritChanceToStealthMultiplicationFactor);
        stats.FlatStatChange("stealth", bonusStealth);
        Debug.Log("NinjaTraining added: " + bonusStealth + " bonusStealth");

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
