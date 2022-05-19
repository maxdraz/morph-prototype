using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agile : PassiveMorph
{
    //[SerializeField] private AgilePrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockCatLike = true;

    Stats stats;
    Jump jump;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        jump = GetComponentInParent<Jump>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);

        if (unlockCatLike)
        {
            CatLike(1);
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ModifyStats(false);

        if (unlockCatLike) 
        {
            CatLike(-1);
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "CatLike")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockCatLike = true;
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

    private void CatLike(int jumps) 
    {
        jump.AddJumps(jumps);
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
