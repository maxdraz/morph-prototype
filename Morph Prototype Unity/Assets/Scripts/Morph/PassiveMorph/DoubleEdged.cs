using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdged : PassiveMorph
{
    static int meleeDamagePrerequisit = 45;
    static int agilityPrerequisit = 30;


    private DamageHandler damageHandler;
    [SerializeField] private float doubleEdgedHPCost;
    [SerializeField] private bool unlockBloodGuzzler;
    bool bloodGuzzlerReady;
    Stats stats;
    float damageBoost;
    float damageBoostFactor = 2;

    //public Prerequisite[] StatPrerequisits;

    private void Start()
    {
        bloodGuzzlerReady = true;
    }

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);

    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "BloodGuzzler")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockBloodGuzzler = true;
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
        if (damageTakenSummary.PhysicalDamage > 0)
        {
            ReduceHPToBoostDamage();
            damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(damageTakenSummary.PhysicalDamage* damageBoost), damageHandler);
        }
        if (unlockBloodGuzzler) 
        {
            if (damageTakenSummary.isMortalBlow && bloodGuzzlerReady) 
            {
                GetComponent<CombatResources>().currentStaminaPoints += damageTakenSummary.PhysicalDamage;
                damageHandler.Health.HealOverTime(damageTakenSummary.PhysicalDamage, 5);
                bloodGuzzlerReady = false;
                StartCoroutine("BloodGuzzlerCooldown");
            }
        }
    }

    IEnumerator BloodGuzzlerCooldown() 
    {
        yield return new WaitForSeconds(20);
        bloodGuzzlerReady = true;
        yield return null;
    }

    private void ReduceHPToBoostDamage() 
    {
        float hpToReduce = damageHandler.Health.currentHealth * doubleEdgedHPCost;
        damageBoost = GetComponent<Stats>().MaxHealth / hpToReduce;
        damageHandler.Health.SubtractHP(hpToReduce);
        damageBoost *= damageBoostFactor;
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
