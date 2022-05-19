using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleReady : PassiveMorph
{
    //[SerializeField] private BattleReadyPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockBattleMaster;
    [SerializeField] private float battleMasterBonusCritChance = 5;


    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);

        if (unlockBattleMaster) 
        {
            stats.globalCritChance += battleMasterBonusCritChance;
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ModifyStats(false);

        if (unlockBattleMaster)
        {
            stats.globalCritChance -= battleMasterBonusCritChance;
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "BattleMaster")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockBattleMaster = true;
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

    private void ChangeRangedDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("rangedDamage", amountToAdd);
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
            

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            

        }

        damageHandler = null;
    }
}
