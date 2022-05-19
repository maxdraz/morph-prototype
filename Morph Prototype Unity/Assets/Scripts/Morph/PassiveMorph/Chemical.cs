using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemical : PassiveMorph
{
    //[SerializeField] private ChemicalPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockConcentratedChemicals;
    [SerializeField] private float concentratedChemicalsBonusToughnessReduction = .1f;
    [SerializeField] private float concentratedChemicalsBonusHealingReduction = .2f;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ModifyStats(false);
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

    public void UnlockSecondary(string name)
    {
        if (name == "ConcentratedChemicals")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockConcentratedChemicals = true;
        }
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void IncreaseAcidDebuff(ref IAcidDamage acidDamage) 
    {
        //acidDamage.toughnessReduction += bonusToughnessReduction;
    }

    private void IncreasePoisonDebuff(ref IPoisonDamage poisonDamage)
    {
        //acidDamage.healingReduction += bonusHealingReduction;
    }

    private void OnAcidDebuffDealt(ref IDamageType damageType, DamageHandler damageTaker)
    {
        if (unlockConcentratedChemicals) 
        {
            if (damageType is IAcidDamage acidDamage)
            {
                IncreaseAcidDebuff(ref acidDamage);
            }    
        }
    }

    private void OnPoisonDebuffDealt(ref IDamageType damageType, DamageHandler damageTaker)
    {
        if (unlockConcentratedChemicals)
        {
            if (damageType is IPoisonDamage poisonDamage)
            {
                IncreasePoisonDebuff(ref poisonDamage);
            }
        }
    }
        

private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            if (unlockConcentratedChemicals) 
            {
                damageHandler.DebuffAboutToBeDealtPreModifier += OnAcidDebuffDealt;
                damageHandler.DebuffAboutToBeDealtPreModifier += OnPoisonDebuffDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            if (unlockConcentratedChemicals)
            {
                damageHandler.DebuffAboutToBeDealtPreModifier -= OnAcidDebuffDealt;
                damageHandler.DebuffAboutToBeDealtPreModifier -= OnPoisonDebuffDealt;
            }
        }

        damageHandler = null;
    }
}
