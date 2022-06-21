using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueStealth : PassiveMorph
{
    static int stealthPrerequisit = 200;
    static int intelligencePrerequisit = 30;

    [SerializeField] private float stealthPenaltyWhileMoving;
    [SerializeField] private int stealthBonusWhileStill;
    private bool moving;

    [SerializeField] private bool unlockHiddenThreat = true;
    [SerializeField] private float hiddenThreatIncreasedDamage;
    [SerializeField] private float hiddenThreatReducedDamage;

    private Velocity velo;
    private Stealth stealth;
    
    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        velo = GetComponent<Velocity>();
        stealth = GetComponent<Stealth>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);

        if (stealth) stealth.stealthModifierWhileMoving += stealthPenaltyWhileMoving;
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);

        if(stealth) stealth.stealthModifierWhileMoving -= stealthPenaltyWhileMoving;
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

    protected override void Update()
    {
        if (velo != null) 
        {
            if (velo.CurrentVelocity.magnitude == 0 && moving)
            {
                moving = false;
                stealth.flatStealthModifier += stealthBonusWhileStill;
            }
            if (velo.CurrentVelocity.magnitude > 0 && !moving)
            {
                moving = true;
                stealth.flatStealthModifier -= stealthBonusWhileStill;
            }
        } 
    }

    public void UnlockSecondary(string name)
    {
        if (name == "HiddenThreat")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockHiddenThreat = true;
        }
    }

    // implement
    private void ChangeStealthStat(int amountToAdd)
    {
        if (stats != null)
        {
            stats.FlatStatChange("stealth", amountToAdd);
        }
    }

    private void OnDamageHasBeenDealt (in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.isStealthAttack)
        {
            damageTakenSummary.PhysicalDamage *= 1 + hiddenThreatIncreasedDamage;
        }
        else 
        {
            damageTakenSummary.PhysicalDamage *= 1 - hiddenThreatReducedDamage;
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
        {
            if (unlockHiddenThreat) 
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
            damageHandler.DamageHasBeenTaken -= OnDamageHasBeenDealt;
        }
    }
}
