using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frills : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockSecondary = true;

    Stats stats;

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);
        stats = GetComponent<Stats>();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
    }

    // implement
    private void ChangeMeleeDamageStat(float amountToAdd)
    {

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
