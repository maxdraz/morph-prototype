using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinisterWatcher : PassiveMorph
{
    static int stealthPrerequisit = 250;
    static int perceptionPrerequisit = 200;

    public DamageHandler targetOfInterest;
    [SerializeField] private float sinisterWatcherMaxRange;
    [SerializeField] private float sinisterWatcherBonusDamage;
    [SerializeField] private float sinisterWatcherMaxBonusDamage;
    [SerializeField] private bool unlockUnkownSource;

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

    public void UnlockSecondary(string name)
    {
        if (name == "UnknownSource")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockUnkownSource = true;
        }
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sinisterWatcherMaxRange);
        float shortestDistance = 0;
        float distance;

        if (hitColliders.Length == 0)
        {
            return;
        }

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stats>() == true)
            {
                distance = (hitCollider.transform.position - transform.position).magnitude;

                if (shortestDistance == 0)
                {
                    sinisterWatcherBonusDamage = 0;
                    shortestDistance = distance;
                    targetOfInterest = hitCollider.GetComponent<DamageHandler>();
                }
                else 
                {

                    if (distance < shortestDistance) 
                    {
                        if (targetOfInterest == hitCollider.GetComponent<DamageHandler>())
                        {
                            sinisterWatcherBonusDamage += Time.deltaTime/10;

                            if (sinisterWatcherBonusDamage > sinisterWatcherMaxBonusDamage) 
                            {
                                sinisterWatcherBonusDamage = sinisterWatcherMaxBonusDamage;
                            }
                        }
                        else
                        {
                            sinisterWatcherBonusDamage = 0;
                            shortestDistance = distance;
                            targetOfInterest = hitCollider.GetComponent<DamageHandler>();
                        }
                    }
                }                
            }
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
        if (damageTakenSummary.PhysicalDamage > 0 && damageTakenSummary.isStealthAttack && damageTakenSummary.DamageTaker == targetOfInterest)
        {
            damageTakenSummary.PhysicalDamage *= 1 + sinisterWatcherBonusDamage;
        }

        if (unlockUnkownSource)
        {
            if (damageTakenSummary.isRangedAttack)
            { 
                //Target should have a harder time finding the location of the attacker
            } 
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();

        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
        }
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
        }

    }
}
