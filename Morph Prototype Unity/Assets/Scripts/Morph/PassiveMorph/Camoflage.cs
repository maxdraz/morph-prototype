using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camoflage : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int stealthStatBonus = 100;
    [SerializeField] private bool unlockSneaky = true;
    public float stealthBonusWhileMoving = .2f;
    Stats stats;
    Stealth stealth;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        stealth = GetComponent<Stealth>();
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeStealthStat(stealthStatBonus);

        if (unlockSneaky) 
        {
            Sneaky(stealthBonusWhileMoving);
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        stealth = GetComponent<Stealth>();

        UnsubscribeFromEvents();
        ChangeStealthStat(-stealthStatBonus);

        if (unlockSneaky)
        {
            Sneaky(-stealthBonusWhileMoving);
        }
    }

    // implement
    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
    }

    private void Sneaky(float amountToAdd)
    {
        stealth.stealthModifierWhileMoving += amountToAdd;
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
            

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            

        }

        damageHandler = null;
    }
}
