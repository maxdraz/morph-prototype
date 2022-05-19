using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoxingSchool : PassiveMorph
{
    static int stealthPrerequisit = 150;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockNinjaTraining;
    [SerializeField] private float shadowBoxingBonusDamageMultiplier = 5;
    float shadowBoxingSchoolBonusDamage;
    Stats stats;

    [SerializeField] private float ninjaTrainingCritChanceToStealthMultiplicationFactor;
    [SerializeField] private float ninjaTrainingAgilityToCritChanceMultiplicationFactor;

    //static Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        ModifyStats(true);

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
        ModifyStats(false);

        
    }

    public void UnlockSecondary(string name)
    {
        if (name == "NinjaTraining")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockNinjaTraining = true;
        }
    }

    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    void ModifyStats(bool AddToStat)
    {
        if (stats != null)
        {
            if (statsToModify.Length > 0)
            {
                for (int i = 0; i <= statsToModify.Length - 1; i++)
                {
                    if (AddToStat)
                    {
                        Debug.Log(GetType().Name + " is adding" + statsToModify[i].value + " to " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), statsToModify[i].value);
                    }
                    else
                    {
                        Debug.Log(GetType().Name + " is removing" + statsToModify[i].value + " from " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), -statsToModify[i].value);
                    }
                }
            }
        }
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
