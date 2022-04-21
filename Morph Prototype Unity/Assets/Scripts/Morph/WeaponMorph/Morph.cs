using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morph : MonoBehaviour
{
    public enum MorphType
    {
        None,
        Melee,
        Ranged,
        Fire,
        Ice,
        Electric,
        Poison,
        Acid,
        Stealth,
        Intimidation
    }

    [SerializeField] private MorphType morphType;
    [SerializeField] private List<PrerequisiteData> prerequisites;


    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    

    public virtual bool CheckPrerequisites(MorphLoadout loadout, int statPrerequisitesToCheck, int morphTypePrerequisiteArrayLength, int morphPrerequisites)
    {
        if (prerequisites == null || prerequisites.Count <= 0) return true;

        bool prerequisitesMet = false;
        foreach (var prerequisiteData in prerequisites)
        {
           // prerequisitesMet = prerequisiteData.Check();
        }
        
        return prerequisitesMet;
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
