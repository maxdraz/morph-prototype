using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigilance : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int perceptionStatBonus = 100;
    [SerializeField] private int stealthStatBonus = 100;
    [SerializeField] private bool unlockHeatVision = true;

    Stats stats;
    public Perception perception;

    private void OnEnable()
    {
        perception = GetComponent<Perception>();
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangePerceptionStat(perceptionStatBonus);
        ChangeStealthStat(stealthStatBonus);
        
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        perception = GetComponent<Perception>();

        UnsubscribeFromEvents();
        ChangePerceptionStat(-perceptionStatBonus);
        ChangeStealthStat(-stealthStatBonus);
    }

    // implement
    private void ChangePerceptionStat(int amountToAdd)
    {
        stats.FlatStatChange("perception", amountToAdd);

    }

    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
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

    private void FixedUpdate()
    {
        if (unlockHeatVision) 
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, perception.maxPerception / 10);

            foreach (var hitCollider in hitColliders)
            {
                //they are auto detected by perception script
            }
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
