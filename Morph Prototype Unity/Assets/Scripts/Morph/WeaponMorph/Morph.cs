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
    };

    public MorphType morphType;
    [SerializeField] private List<PrerequisiteData> prerequisites;

    //This was to be used with activating secondaries as a precedural name for secondaryunlock bools
    //public Dictionary<string, bool> boolHolder = new Dictionary<string, bool>();

    public StatValue[] statsToModify;

    protected virtual void Awake()
    {
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Start()
    {
        //if (CountPrerequisiteListLength() > 1)
        //{
        //    CheckSecondaryPrerequisites(GetComponent<MorphLoadout>(), GetComponent<Stats>());
        //}
    }

    int PrerequisiteListCount() 
    {
        int prerequisiteListLength;

        prerequisiteListLength = prerequisites.Count;

        return prerequisiteListLength;
    }

   //int SecondaryListCount() 
   //{
   //    int secondaryListLength = 0;
   //
   //    foreach (PrerequisiteData prerequisite in prerequisites) 
   //    {
   //    if (prerequisite.isSecondary) 
   //        {
   //            secondaryListLength++;
   //        }
   //    }
   //
   //    return secondaryListLength;
   //}

    void UnlockSecondary(string name) 
    {
        Debug.Log(this.name + " is trying to: " + "unlock" + name);
        //boolHolder["unlock" + name] = true;
        BroadcastMessage("UnlockSecondary", name);
        
    }

    public virtual bool CheckPrerequisites(MorphLoadout loadout, Stats stats, Morph morphPrefab)
    {
        if (prerequisites == null || prerequisites.Count <= 0) return true;

        bool primaryPrerequisitesMet = false;

        if (prerequisites[0].isSecondary == true)
        {
            primaryPrerequisitesMet = true;
        }
        else 
        {
            primaryPrerequisitesMet = prerequisites[0].CheckPrerequisites(loadout, stats, morphPrefab);
        }

        if (primaryPrerequisitesMet) 
        {
            
            CheckSecondaryPrerequisites(loadout, stats);
        }

        return primaryPrerequisitesMet;
    }
    
    void CheckSecondaryPrerequisites(MorphLoadout loadout, Stats stats)
    {
        string[] secondariesToUnlock = new string[prerequisites.Count];

        for (int i = 0; i < prerequisites.Count; i++)
        {
            secondariesToUnlock[i] = prerequisites[i].CheckSecondaryPrerequisites(loadout, stats);
            Debug.Log("SecondaryPrerequisites check for " + secondariesToUnlock[i] + " returned true");
        }

        for (int i = 0; i < secondariesToUnlock.Length; i++) 
        {
            if (secondariesToUnlock[i] != null)
            {
                Debug.Log("CheckSecondaryPrerequisites succeeded for " + secondariesToUnlock[i]);
                UnlockSecondary(secondariesToUnlock[i]);
            }
        }
    }




    public string GetMorphType() 
    {
        string type = null;

        if (morphType == MorphType.None)
        {
            //Debug.Log(GetType().Name + " is of type: None");
            type = "None";
        }

        else if (morphType == MorphType.Poison)
        {
            //Debug.Log(name + " is of type: Poison");
            type = "Poison";
        }

        else if (morphType == MorphType.Fire)
        {
            //Debug.Log(name + " is of type: Fire");
            type = "Fire";
        }

        else if (morphType == MorphType.Ice)
        {
            //Debug.Log(name + " is of type: Ice");
            type = "Ice";
        }

        else if (morphType == MorphType.Acid)
        {
            //Debug.Log(name + " is of type: Acid");
            type = "Acid";
        }

        else if (morphType == MorphType.Electric)
        {
            //Debug.Log(name + " is of type: Electric");
            type = "Electric";
        }

        else if (morphType == MorphType.Melee)
        {
            //Debug.Log(name + " is of type: Melee");
            type = "Melee";
        }

        else if (morphType == MorphType.Ranged)
        {
            //Debug.Log(name + " is of type: Ranged");
            type = "Ranged";
        }

        else if (morphType == MorphType.Stealth)
        {
            //Debug.Log(name + " is of type: Stealth");
            type = "Stealth";
        }

        else if (morphType == MorphType.Intimidation)
        {
            //Debug.Log(name + " is of type: Intimidation");
            type = "Intimidation";
        }

        else if (type == null) 
        {
            type = "None";
        }
        return type;
    }
}
