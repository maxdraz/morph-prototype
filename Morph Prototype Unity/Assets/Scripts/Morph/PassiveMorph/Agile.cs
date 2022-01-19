using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agile : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int agilityStatBonus = 5;
    [SerializeField] private bool unlockSecondary = true;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeAgilityStat(agilityStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeAgilityStat(-agilityStatBonus);
    }

    // implement
    private void ChangeAgilityStat(int amountToAdd)
    {
        stats.FlatStatChange("agility", amountToAdd);
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
