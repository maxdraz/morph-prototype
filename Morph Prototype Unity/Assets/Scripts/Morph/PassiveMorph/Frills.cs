using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frills : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int intimidationStatBonus = 5;
    [SerializeField] private bool unlockFearless = true;
    [SerializeField] private float intimidationDefenseBonus = .3f;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeIntimidationStat(intimidationStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeIntimidationStat(-intimidationStatBonus);
    }

    // implement
    private void ChangeIntimidationStat(int amountToAdd)
    {
        stats.FlatStatChange("intimidation",amountToAdd);
    }

    private void Fearless(float amount) 
    {
    //GetComponent<Intimidation>().defenseModifier += amount;
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
