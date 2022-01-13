using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganglia : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int intelligenceStatBonus = 5;
    [SerializeField] private bool unlockConditioning = true;

    Stats stats;

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(intelligenceStatBonus);
        stats = GetComponent<Stats>();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-intelligenceStatBonus);
    }

    // implement
    private void ChangeMeleeDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("intelligence",amountToAdd);
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
