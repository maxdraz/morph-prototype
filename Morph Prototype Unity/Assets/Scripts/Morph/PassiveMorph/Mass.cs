using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : PassiveMorph
{
    //[SerializeField] private MassPrerequisiteData prerequisiteData;

    private DamageHandler damageHandler;
    [SerializeField] private float percentHealthBonus = .20f;
    [SerializeField] private bool unlockRestoration;

    [SerializeField] private float restorationPercentHealthRegen = .02f;

    [SerializeField] private Health health;

    private void OnEnable()
    {
        health = GetComponent<Health>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMaxHealthStat(percentHealthBonus);
        if (unlockRestoration) 
        {
            StartCoroutine("Restoration");
        }
    }

    private void OnDisable()
    {
        health = GetComponent<Health>();

        UnsubscribeFromEvents();
        ChangeMaxHealthStat(-percentHealthBonus);
        if (unlockRestoration)
        {
            StopCoroutine("Restoration");
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Restoration")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockRestoration = true;
        }
    }

    IEnumerator Restoration() 
    {
        yield return new WaitForSeconds(1);

        transform.parent.gameObject.BroadcastMessage("AddPercentHP", restorationPercentHealthRegen);

        StartCoroutine("Restoration");

        yield return null;
    }

    // implement
    private void ChangeMaxHealthStat(float amountToAdd)
    {
        health.maxHealthBonus += amountToAdd;
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
