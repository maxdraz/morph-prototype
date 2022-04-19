using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineRush : ActiveMorph
{
    static int fortitudePrerequisit = 35;
    [SerializeField] private PrerequisiteData prerequisiteData;
    [SerializeField] private GameObject adrenalineRushParticles;
    [SerializeField] private float adrenalineBoost;

    //static Prerequisite[] StatPrerequisits;

    private void Start()
    {
        //WriteToPrerequisiteArray();
    }

    //void WriteToPrerequisiteArray()
    //{
    //    statPrerequisits = new Prerequisite[StatPrerequisits.Length];
    //
    //    for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
    //    {
    //        statPrerequisits[i] = StatPrerequisits[i];
    //        Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
    //    }
    //}

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            AdrenalineBoost();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            AdrenalineBoost();
        }
    }

    private void AdrenalineBoost()
    {
        GameObject boost = ObjectPooler.Instance.GetOrCreatePooledObject(adrenalineRushParticles);
        boost.transform.position = transform.position;
        boost.transform.parent = transform;
        GetComponent<Stamina>().AddStamina(adrenalineBoost);
    }

    public override bool CheckPrerequisites(MorphLoadout loadout, int statPrerequisiteArrayLength, int morphTypePrerequisiteArrayLength, int morphPrerequisites) 
    {
        if (statPrerequisiteArrayLength > 0) 
        {
            if (CheckStatPrerequisites(statPrerequisiteArrayLength) == true)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        if (morphTypePrerequisiteArrayLength > 0) 
        {
            
            if (CheckTypePrerequisites(morphTypePrerequisiteArrayLength, loadout) == true) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        if (morphPrerequisites > 0) 
        {
            if (CheckMorphPrerequisites(morphPrerequisites, loadout) == true)
            {
                return true;
            } 
            else 
            {
                return false;
            } 
        }

        return true;
    }

    private bool CheckStatPrerequisites(int statPrerequisiteArrayLength)
    {
        int positiveResults = statPrerequisiteArrayLength;

        if (!prerequisiteData)
        {
            Debug.Log(gameObject.name + " no " + GetType().ToString() + " statPerequisite data assigned!");
            return false;
        }

        for (int i = 0; i <= statPrerequisiteArrayLength - 1; i++)
        {
            // string statName = Enum.GetName(typeof(StatPrerequisite), prerequisiteData.AdrenalineRushStatPrerequisites[i]);
            //
            //  if (GetComponent<Stats>().FindStatValue(statName) >= prerequisiteData.AdrenalineRushStatPrerequisites[i].value) 
            //  {
            //      positiveResults++;
            //
            //  }
        }

        if (positiveResults == statPrerequisiteArrayLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckTypePrerequisites(int morphTypePrerequisiteArrayLength, MorphLoadout loadout) 
    {
        int positiveResults = morphTypePrerequisiteArrayLength;

        if (!prerequisiteData)
        {
            Debug.Log(gameObject.name + " no " + GetType().ToString() + " typePrerequisite data assigned!");
            return false;
        }

        for (int i = 0; i <= morphTypePrerequisiteArrayLength - 1; i++)
        {
             // if (loadout.GetMorphsByType(prerequisiteData.AdrenalineRushTypePrerequisites[i].type.ToString(), prerequisiteData.AdrenalineRushTypePrerequisites[i].amount) == true) 
             // {
             //     positiveResults++;
             // }
        }

        if (positiveResults == morphTypePrerequisiteArrayLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckMorphPrerequisites(int morphPrerequisiteArrayLength, MorphLoadout loadout)
    {
        int positiveResults = morphPrerequisiteArrayLength;

        if (!prerequisiteData)
        {
            Debug.Log(gameObject.name + " no " + GetType().ToString() + " morphPerequisite data assigned!");
            return false;
        }

        for (int i = 0; i <= morphPrerequisiteArrayLength - 1; i++)
        {
            // if (loadout.GetPrerequisiteMorphByName(prerequisiteData.AdrenalineRushMorphPrerequisites[i].name) == true)
            // {
            //     positiveResults++;
            // }
        }

        if (positiveResults == morphPrerequisiteArrayLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
