using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SureShot : PassiveMorph
{
    //[SerializeField] private SureShotPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private int rangedDamageStatBonus = 5;
    [SerializeField] private bool unlockExpandedReserves = true;
    [SerializeField] private float maxEnergyStatBonus = .2f;

    [SerializeField] private Energy energy;
    Stats stats;
    
    
    

    private void OnEnable()
    {
        energy = GetComponent<Energy>();
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        AddToStatValue(statToAddTo.ToString(), statBonus);


        if (unlockExpandedReserves) 
        {
            ChangeMaxEnergyStat(maxEnergyStatBonus);
        }
    }

    private void OnDisable()
    {
        energy = GetComponent<Energy>();
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        AddToStatValue(statToAddTo.ToString(), -statBonus);

        if (unlockExpandedReserves)
        {
            ChangeMaxEnergyStat(-maxEnergyStatBonus);
        }
    }

    public void UnlockSecondary (string name) 
    {
        if (name == "ExpandedReserves") 
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockExpandedReserves = true;
        }      
    }

    // implement
    void AddToStatValue(string statName, int value)
    {
        if (stats != null)
        {
            if (statName != null && statBonus != 0)
            {
                Debug.Log("Adding to " + statName);
                stats.FlatStatChange(statName, value);
            }
        }
    }

    private void ChangeMaxEnergyStat(float amountToAdd)
    {
        energy.bonusMaxEnergy += amountToAdd;
        energy.SetMaxEnergy();
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
