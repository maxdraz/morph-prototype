using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueStealth : PassiveMorph
{
    static int stealthPrerequisit = 200;
    static int intelligencePrerequisit = 30;


    private DamageHandler damageHandler;
    [SerializeField] private int stealthStatBonus = 5;
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
        AddToStatValue(statToAddTo.ToString(), statBonus);

        if (stealth != null) 
        {
            stealth.stealthModifierWhileMoving += stealthPenaltyWhileMoving;
        }
    }

   

    private void OnDisable()
    {

        UnsubscribeFromEvents();
        AddToStatValue(statToAddTo.ToString(), -statBonus);

        GetComponent<Stealth>().stealthModifierWhileMoving -= stealthPenaltyWhileMoving;

    }

    void AddToStatValue(string statName, int value)
    {
        if (stats != null)
        {
            if (statName != null && statBonus != 0)
            {
                Debug.Log("Adding to " + statName);
                stats.FlatStatChange(statName, value);
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
