using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Morph Prerequisite Data", menuName = "Morph Prerequisite Data/Prerequisite Data")]
public class PrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] statPrerequisites;
    public MorphTypePrerequisite[] typePrerequisites;
    public Morph[] morphPrerequisites;

    public bool CheckPrerequisites(MorphLoadout loadout, Stats stats)
    {
        if (statPrerequisites.Length > 0)
        {
            if (CheckStatPrerequisites(stats) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (typePrerequisites.Length > 0)
        {

            if (CheckTypePrerequisites(loadout) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (morphPrerequisites.Length > 0)
        {
            if (CheckMorphPrerequisites(loadout) == true)
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

    private bool CheckStatPrerequisites(Stats stats)
    {
        int positiveResults = 0;

        for (int i = 0; i <= statPrerequisites.Length - 1; i++)
        {
            if (stats.FindStatValue(statPrerequisites[i].stat.ToString()) >= statPrerequisites[i].value)
            {
                positiveResults++;
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
        
        for (int i = 0; i <= typePrerequisites.Length - 1; i++)
        {
              if (loadout.GetMorphsByType(typePrerequisites[i].type.ToString(), typePrerequisites[i].amount) == true) 
              {
                  positiveResults++;
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
        
        for (int i = 0; i <= morphPrerequisites.Length - 1; i++)
        {
             if (loadout.GetPrerequisiteMorphByName(morphPrerequisites[i].name) == true)
             {
                 positiveResults++;
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


