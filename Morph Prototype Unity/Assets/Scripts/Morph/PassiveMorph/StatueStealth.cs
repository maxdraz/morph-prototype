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
        ChangeStealthStat(stealthStatBonus);

        stealth.stealthModifierWhileMoving += stealthPenaltyWhileMoving; 
    }

   

    private void OnDisable()
    {

        UnsubscribeFromEvents();
        ChangeStealthStat(-stealthStatBonus);

        GetComponent<Stealth>().stealthModifierWhileMoving -= stealthPenaltyWhileMoving;

    }

    private void Update()
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

    // implement
    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void OnDamageAboutToBeDealt (ref IDamageType damageType)
    {
        //if (stealthAttack)
        {
            //physicalDamageToBeDealt *= 1 + hiddenThreatIncreasedDamage;
        }
        //else 
        {
            //physicalDamageToBeDeat *= 1 - hiddenThreatReducedDamage;
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
                damageHandler.DamageAboutToBeDealt += OnDamageAboutToBeDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageAboutToBeTaken -= OnDamageAboutToBeDealt;

        }

        damageHandler = null;
    }
}
