using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueStealth : PassiveMorph
{
    static int stealthPrerequisit = 200;
    static int intelligencePrerequisit = 30;


    private DamageHandler damageHandler;
    [SerializeField] private float stealthPenaltyWhileMoving;
    [SerializeField] private int stealthBonusWhileStill;
    bool moving;

    [SerializeField] private bool unlockHiddenThreat = true;
    [SerializeField] private float hiddenThreatIncreasedDamage;
    [SerializeField] private float hiddenThreatReducedDamage;


    Stats stats;
    Velocity velo;
    Stealth stealth;

    //public Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        velo = GetComponent<Velocity>();
        stealth = GetComponent<Stealth>();
        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);

        if (stealth != null) 
        {
            stealth.stealthModifierWhileMoving += stealthPenaltyWhileMoving;
        }
    }

   

    private void OnDisable()
    {

        UnsubscribeFromEvents();
        ModifyStats(false);

        GetComponent<Stealth>().stealthModifierWhileMoving -= stealthPenaltyWhileMoving;

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

    private void Update()
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

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
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

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            if (unlockHiddenThreat) 
            {
                damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenTaken -= OnDamageHasBeenDealt;

        }

        damageHandler = null;
    }
}
