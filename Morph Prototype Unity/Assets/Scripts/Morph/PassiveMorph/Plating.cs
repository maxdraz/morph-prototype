using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plating : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float bonusMaxArmor = 100;
    [SerializeField] private bool unlockCriticalCoverage = true;

    [SerializeField] private float critChanceResist = .1f;
    [SerializeField] private float bleedingResist = .2f;

    Stats stats;
    Armor armor;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        armor = GetComponent<Armor>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangArmorStat(bonusMaxArmor);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        armor = GetComponent<Armor>();

        UnsubscribeFromEvents();
        ChangArmorStat(-bonusMaxArmor);
    }

    private void ChangArmorStat(float amountToAdd)
    {
        //Debug.Log("Plating adding: " + amountToAdd + " to armor stat");
        armor.bonusFlatMaxArmor += amountToAdd;
    }

    private void OnDamageAboutToBeTaken(in DamageTakenSummary damageTakenSummary) 
    {
        //damageTakenSummary.critChance -= critChanceResist;
        //damageTakenSummary.bleedingValue -= bleedingResist;
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
