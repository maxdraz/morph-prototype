using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morph : MonoBehaviour
{
    public Morph[] morphPrerequisites;

    //public Prerequisite[] statPrerequisits;

    public bool CheckMorphPrerequisites(MorphLoadout loadout)
    {
        int morphPrerequisitesCount = 0;

        if (morphPrerequisites.Length == 0)
            return true;

        if (morphPrerequisites.Length > 0)
        {
            foreach (Morph morph in morphPrerequisites)
            {
                if (loadout.GetPrerequisiteMorphByName(morph.name.ToString()) == true)
                {
                    morphPrerequisitesCount++;
                }
                
            }
        }
        if (morphPrerequisitesCount == morphPrerequisites.Length)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public virtual bool CheckStatPrerequisites(MorphLoadout loadout, int statPrerequisitesToCheck)
    {
        return true;
    }

    // public bool CheckStatPrerequisites(Stats stats) {
   //
   //     int statPrerequisitesCount = 0;
   //
   //     if (statPrerequisits.Length == 0)
   //         return true;
   //
   //     if (statPrerequisits.Length > 0) 
   //         {
   //         foreach (Prerequisite prerequisite in statPrerequisits)
   //         {
   //             if (stats.FindStatValue(prerequisite.stat) >= prerequisite.value)
   //             {
   //                 statPrerequisitesCount++;
   //             }
   //         }
   //     }
   //     if (statPrerequisitesCount == statPrerequisits.Length)
   //     {
   //         return true;
   //     }
   //     else 
   //     {
   //         return false;
   //     } 
   // }
}
