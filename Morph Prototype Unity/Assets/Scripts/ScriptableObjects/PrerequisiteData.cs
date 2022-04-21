using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Morph Prerequisite Data", menuName = "Morph Prerequisite Data/Prerequisite Data")]
public class PrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] statPrerequisites;
    public MorphTypePrerequisite[] typePrerequisites;
    public Morph[] morphPrerequisites;

    public bool CheckPrerequisites(MorphLoadout loadout, int statPrerequisiteArrayLength, int morphTypePrerequisiteArrayLength, int morphPrerequisites)
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
        //int positiveResults = statPrerequisiteArrayLength;
        //
        //if (!prerequisiteData)
        //{
        //    Debug.Log(gameObject.name + " no " + GetType().ToString() + " statPerequisite data assigned!");
        //    return false;
        //}
        //
        //for (int i = 0; i <= statPrerequisiteArrayLength - 1; i++)
        //{
        //    // string statName = Enum.GetName(typeof(StatPrerequisite), prerequisiteData.AdrenalineRushStatPrerequisites[i]);
        //    //
        //    //  if (GetComponent<Stats>().FindStatValue(statName) >= prerequisiteData.AdrenalineRushStatPrerequisites[i].value) 
        //    //  {
        //    //      positiveResults++;
        //    //
        //    //  }
        //}
        //
        //if (positiveResults == statPrerequisiteArrayLength)
        //{
        //    return true;
        //}
        //else
        //{
        return false;
        //}
    }

    private bool CheckTypePrerequisites(int morphTypePrerequisiteArrayLength, MorphLoadout loadout)
    {
        //int positiveResults = morphTypePrerequisiteArrayLength;
        //
        //if (!prerequisiteData)
        //{
        //    Debug.Log(gameObject.name + " no " + GetType().ToString() + " typePrerequisite data assigned!");
        //    return false;
        //}
        //
        //for (int i = 0; i <= morphTypePrerequisiteArrayLength - 1; i++)
        //{
        //     // if (loadout.GetMorphsByType(prerequisiteData.AdrenalineRushTypePrerequisites[i].type.ToString(), prerequisiteData.AdrenalineRushTypePrerequisites[i].amount) == true) 
        //     // {
        //     //     positiveResults++;
        //     // }
        //}
        //
        //if (positiveResults == morphTypePrerequisiteArrayLength)
        //{
        //    return true;
        //}
        //else
        //{
        return false;
        //}
    }

    private bool CheckMorphPrerequisites(int morphPrerequisiteArrayLength, MorphLoadout loadout)
    {
        //int positiveResults = morphPrerequisiteArrayLength;
        //
        //if (!prerequisiteData)
        //{
        //    Debug.Log(gameObject.name + " no " + GetType().ToString() + " morphPerequisite data assigned!");
        //    return false;
        //}
        //
        //for (int i = 0; i <= morphPrerequisiteArrayLength - 1; i++)
        //{
        //    // if (loadout.GetPrerequisiteMorphByName(prerequisiteData.AdrenalineRushMorphPrerequisites[i].name) == true)
        //    // {
        //    //     positiveResults++;
        //    // }
        //}
        //
        //if (positiveResults == morphPrerequisiteArrayLength)
        //{
        //    return true;
        //}
        //else
        //{
        return false;
        //}
    }
}


