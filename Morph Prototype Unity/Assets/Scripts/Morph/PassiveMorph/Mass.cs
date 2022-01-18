using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float percentHealthBonus = .20f;
    [SerializeField] private bool unlockRestoration = true;

    //Restoration gives % MAXHP regen, .1 or 10% value here turns into 2% MAXHP healed/second. Value = percentHealthRegen/5
    [SerializeField] private float percentHealthRegen = .1f;

    Health health;

    private void OnEnable()
    {
        health = transform.gameObject.GetComponent<Health>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMaxHealthStat(percentHealthBonus);
        if (unlockRestoration) 
        {
            StartCoroutine("Restoration");
        }
    }

    private void OnDisable()
    {
        health = transform.gameObject.GetComponent<Health>();

        UnsubscribeFromEvents();
        ChangeMaxHealthStat(-percentHealthBonus);
        if (unlockRestoration)
        {
            StopCoroutine("Restoration");
        }
    }

    IEnumerator Restoration() 
    {
        yield return new WaitForSeconds(1);

        BroadcastMessage("AddPercentHP", percentHealthRegen);

        StartCoroutine("Restoration");

        yield return null;
    }

    // implement
    private void ChangeMaxHealthStat(float amountToAdd)
    {
        health.maxHealthBonus += amountToAdd;
        BroadcastMessage("SetMaxHP");
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
