using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awareness : PassiveMorph
{
    [SerializeField] private bool unlockKeenSenses = true;

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);
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

    public void UnlockSecondary(string name)
    {
        if (name == "KeenSenses")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockKeenSenses = true;
        }
    }
}
