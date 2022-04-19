using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : PassiveMorph
{
    //[SerializeField] private ElementalPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private int elementalDamageStatBonus = 5;
    [SerializeField] private bool unlockForceOfNature = true;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeElementalDamageStat(elementalDamageStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeElementalDamageStat(-elementalDamageStatBonus);
    }

    // implement
    private void ChangeElementalDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("elementalDamage", amountToAdd);
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (unlockForceOfNature) 
        {
            if (damageTakenSummary.FireDamage > 0)
            {
                //need to add to the targets 'burning' bar based on the the fire damage dealt
            }

            if (damageTakenSummary.IceDamage > 0)
            {
                //need to add to the targets 'frozen' bar based on the the fire damage dealt
            }


            //if (damageTakenSummary.ElectricDamage > 0)
            //{
                //need to add to the targets 'electrified' bar based on the the fire damage dealt
            //}
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
            if (unlockForceOfNature) 
            {
                damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {

            if (unlockForceOfNature)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            }
        }

        damageHandler = null;
    }
}
