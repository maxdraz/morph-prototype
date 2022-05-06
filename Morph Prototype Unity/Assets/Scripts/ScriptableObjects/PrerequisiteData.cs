using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Morph Prerequisite Data", menuName = "Morph Prerequisite Data/Prerequisite Data")]
public class PrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] statPrerequisites;
    public MorphTypePrerequisite[] typePrerequisites;
    public Morph[] morphPrerequisites;

    public bool CheckPrerequisites(MorphLoadout loadout, Stats stats, Morph morphPrefab)
    {
        bool statsCheck = false;
        bool typeCheck = false;
        bool morphCheck = false;

        if (statPrerequisites.Length > 0)
        {
            if (CheckStatPrerequisites(stats) == true)
            {
                statsCheck = true;
            }
        }
        else 
        {
            statsCheck = true;
        }

        if (typePrerequisites.Length > 0)
        {

            if (CheckTypePrerequisites(loadout) == true)
            {
                return true;
            }
        }
        else
        {
            typeCheck = true;
        }

        if (morphPrerequisites.Length > 0)
        {
            if (CheckMorphPrerequisites(loadout) == true)
            {
                return true;
            }
        }
        else
        {
            morphCheck = true;
        }

        if (statsCheck == true && typeCheck == true && morphCheck == true) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    private bool CheckStatPrerequisites(Stats stats)
    {
        int positiveResults = 0;

        if (statPrerequisites.Length > 0) 
        {
            for (int i = 0; i <= statPrerequisites.Length - 1; i++)
            {
                if (stats.FindStatValue(statPrerequisites[i].stat.ToString()) >= statPrerequisites[i].value)
                {
                    positiveResults++;
                }
                else
                {
                    Debug.Log("Player character does not have enough " + statPrerequisites[i].stat.ToString() + " to attach " + name);
                }
            }
        }
        
        if (positiveResults == statPrerequisites.Length) 
        {
            return true;
        }
        else
        {

            return false;
        }
    }

    private bool CheckTypePrerequisites(MorphLoadout loadout)
    {
        int positiveResults = 0;

        if (typePrerequisites.Length > 0)
        {
            for (int i = 0; i <= typePrerequisites.Length - 1; i++)
            {
                if (loadout.GetMorphsByType(typePrerequisites[i].type.ToString()) == typePrerequisites[i].amount)
                {
                    positiveResults++;
                }
                else
                {
                    Debug.Log("Not enough morphs of type: " + typePrerequisites[i].type.ToString() + " attached to player character to attach " + name);
                }
            }
        }

        if (positiveResults == typePrerequisites.Length)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    private bool CheckMorphPrerequisites(MorphLoadout loadout)
    {
        int positiveResults = 0;

        if (morphPrerequisites.Length > 0)
        {
            for (int i = 0; i <= morphPrerequisites.Length - 1; i++)
            {
                if (loadout.GetPrerequisiteMorphByName(morphPrerequisites[i].name) == true)
                {
                    positiveResults++;
                }
                else
                {
                    Debug.Log("Player character does not have " + morphPrerequisites[i].name + "which is needed to attach " + name);
                }
            }
        }   
        
        if (positiveResults == morphPrerequisites.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


