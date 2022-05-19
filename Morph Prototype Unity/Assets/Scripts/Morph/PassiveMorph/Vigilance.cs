using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigilance : PassiveMorph
{
    //[SerializeField] private VigilancePrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockHeatVision;

    Stats stats;
    public Perception perception;

    private void OnEnable()
    {
        perception = GetComponent<Perception>();
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);
        
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        perception = GetComponent<Perception>();

        UnsubscribeFromEvents();
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "HeatVision")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockHeatVision = true;
        }
    }

    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    void ModifyStats(bool AddToStat)
    {
        if (stats != null)
        {
            if (statsToModify.Length > 0)
            {
                for (int i = 0; i <= statsToModify.Length - 1; i++)
                {
                    if (AddToStat)
                    {
                        Debug.Log(GetType().Name + " is adding" + statsToModify[i].value + " to " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), statsToModify[i].value);
                    }
                    else
                    {
                        Debug.Log(GetType().Name + " is removing" + statsToModify[i].value + " from " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), -statsToModify[i].value);
                    }
                }
            }
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
