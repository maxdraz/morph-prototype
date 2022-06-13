using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleReady : PassiveMorph
{
    [SerializeField] private bool unlockBattleMaster;
    [SerializeField] private float battleMasterBonusCritChance = 5;

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);
        if (unlockBattleMaster) 
        {
            stats.globalCritChance += battleMasterBonusCritChance;
        }
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
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
}
