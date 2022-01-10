using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugged : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float toughnessStatBonus = 5;

    [SerializeField] private bool unlockUnbreakable = true;
    [SerializeField] private float flatPhysicalDamageReduction;

    Stats stats;

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeToughnessStat(toughnessStatBonus);
        stats = GetComponent<Stats>();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeToughnessStat(-toughnessStatBonus);
    }

    // implement
    private void ChangeToughnessStat(float amountToAdd)
    {

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
