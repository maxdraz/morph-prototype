using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementalist : PassiveMorph
{
    [SerializeField] private bool unlockForceOfNature = true;

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

    // implement
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
        if (name == "ForceOfNature")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockForceOfNature = true;
        }
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (unlockForceOfNature) 
        {
            if (damageTakenSummary.FireDamage > 0)
            {
                //need to add to the targets 'burning' bar based on the the fire damage dealt
            }

            if (damageTakenSummary.IceDamage > 0)
            {
                //need to add to the targets 'frozen' bar based on the the fire damage dealt
            }


            //if (damageTakenSummary.ElectricDamage > 0)
            //{
                //need to add to the targets 'electrified' bar based on the the fire damage dealt
            //}
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
        {
            if (unlockForceOfNature) 
            {
                damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
            }
        }
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            if (unlockForceOfNature)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            }
        }
    }
}
