using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awareness : PassiveMorph
{
    //[SerializeField] private AwarenessPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private float perceptionStatBonus = 5;
    [SerializeField] private bool unlockSecondary = true;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangePerceptionStat(perceptionStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangePerceptionStat(-perceptionStatBonus);
    }

    // implement
    private void ChangePerceptionStat(float amountToAdd)
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
