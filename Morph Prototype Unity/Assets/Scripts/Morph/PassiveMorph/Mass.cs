using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float percentHealthBonus = .20f;
    [SerializeField] private bool unlockRestoration = true;
    [SerializeField] private float healthRegen = 5;
    Stats stats;

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeHealthStat(percentHealthBonus);
        if (unlockRestoration) 
        {
            //stats.healthRegen += healthRegen; 
        }
        stats = GetComponent<Stats>();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeHealthStat(-percentHealthBonus);
        if (unlockRestoration)
        {
            //stats.healthRegen -= healthRegen; 
        }
    }

    // implement
    private void ChangeHealthStat(float amountToAdd)
    {
        //stats.maxHealth = stats.maxHealth * (1 + percentHealthBonus);
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
