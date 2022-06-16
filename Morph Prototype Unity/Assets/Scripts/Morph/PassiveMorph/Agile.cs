using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agile : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private bool unlockCatLike = true;
    private Jump jump;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        jump = GetComponentInParent<Jump>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);

        if (unlockCatLike)
        {
            CatLike(1);
        }
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);

        if (unlockCatLike) 
        {
            CatLike(-1);
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "CatLike")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockCatLike = true;
        }
    }

    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    private void ModifyStats(bool AddToStat)
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

    private void CatLike(int jumps) 
    {
        jump.AddJumps(jumps);
    }
}
