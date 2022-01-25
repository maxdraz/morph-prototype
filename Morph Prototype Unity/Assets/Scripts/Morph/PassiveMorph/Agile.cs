using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agile : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int agilityStatBonus = 5;
    [SerializeField] private bool unlockCatLike = true;

    Stats stats;
    Jump jump;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        jump = GetComponentInParent<Jump>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeAgilityStat(agilityStatBonus);

        if (unlockCatLike)
        {
            CatLike(1);
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeAgilityStat(-agilityStatBonus);

        if (unlockCatLike) 
        {
            CatLike(-1);
        }
    }

    // implement
    private void ChangeAgilityStat(int amountToAdd)
    {
        stats.FlatStatChange("agility", amountToAdd);
    }

    private void CatLike(int jumps) 
    {
        jump.AddJumps(jumps);
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
