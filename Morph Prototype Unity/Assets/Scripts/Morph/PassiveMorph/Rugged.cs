using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugged : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int toughnessStatBonus = 5;

    [SerializeField] private bool unlockUnbreakable = true;
    [SerializeField] private float flatPhysicalDamageReduction;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeToughnessStat(toughnessStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeToughnessStat(-toughnessStatBonus);
    }

    // implement
    private void ChangeToughnessStat(int amountToAdd)
    {
        stats.FlatStatChange("toughness", amountToAdd);
    }

    private void OnDamageAboutToBeTaken(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0 && unlockUnbreakable)
        {
            damageTakenSummary.PhysicalDamage -= flatPhysicalDamageReduction;
        }
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
            //damageHandler.DamageAboutToBeTaken += OnDamageAboutToBeTaken;

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            //damageHandler.DamageAboutToBeTaken -= OnDamageAboutToBeTaken;

        }

        damageHandler = null;
    }
}
