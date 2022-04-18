using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morph : MonoBehaviour
{
    public enum MorphType
    {
        Melee,
        Ranged,
        Fire,
        Ice,
        Electric,
        Poison,
        Acid
    }


    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }

    

    public virtual bool CheckPrerequisites(MorphLoadout loadout, int statPrerequisitesToCheck, int morphTypePrerequisiteArrayLength, int morphPrerequisites)
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
