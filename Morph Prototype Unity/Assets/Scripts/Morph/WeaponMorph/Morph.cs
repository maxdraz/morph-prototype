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
    Dictionary<string, bool> boolHolder = new Dictionary<string, bool>();



    protected void Awake()
    {

        //Debug.Log(GetType().Name + " has " + prerequisites.Count + " prerequisites");
        //CountPrerequisiteListLength();
    }

    protected virtual void Start()
    {
        //if (CountPrerequisiteListLength() > 1)
        //{
        //    CheckSecondaryPrerequisites(GetComponent<MorphLoadout>(), GetComponent<Stats>());
        //}
    }

    protected virtual void Update()
    {
        
    }

    public int PrerequisiteListLength() 
    {
        int prerequisiteListLength;

        prerequisiteListLength = prerequisites.Count;

        return prerequisiteListLength;
    }

    void UnlockSecondary(string name) 
    {
        Debug.Log(this.name + " is trying to: " + "unlock" + name);
        boolHolder["unlock" + name] = true;
        //BroadcastMessage("Unlock" + name);
        
    }

    public virtual bool CheckPrerequisites(MorphLoadout loadout, Stats stats, Morph morphPrefab)
    {
        if (prerequisites == null || prerequisites.Count <= 0) return true;

        bool primaryPrerequisitesMet = false;


        primaryPrerequisitesMet = prerequisites[0].CheckPrerequisites(loadout, stats, morphPrefab);

        if (primaryPrerequisitesMet && PrerequisiteListLength() > 1) 
        {
            
            CheckSecondaryPrerequisites(loadout, stats);
        }

        return primaryPrerequisitesMet;
    }
    
    void CheckSecondaryPrerequisites(MorphLoadout loadout, Stats stats)
    {
        bool[] secondariesToUnlock = new bool[PrerequisiteListLength() -1];
        Debug.Log(this.name + " has " + secondariesToUnlock.Length + " secondaries");

        for (int i = 1; i <= secondariesToUnlock.Length; i++)
        {
            Debug.Log("Going to checkSecondaryPrerequisites for " + prerequisites[i].name);
            secondariesToUnlock[i - 1] = prerequisites[i].CheckSecondaryPrerequisites(loadout, stats);
            Debug.Log("SecondaryPrerequisites check for " + prerequisites[i].name + " returned " + secondariesToUnlock[i - 1].ToString());
        }

        for (int i = 0; i < secondariesToUnlock.Length; i++) 
        {
            if (secondariesToUnlock[i] == true)
            {
                Debug.Log("CheckSecondaryPrerequisites succeeded for " + prerequisites[i + 1]);
                UnlockSecondary(prerequisites[i + 1].name);
            }
            else
            {
                Debug.Log("CheckSecondaryPrerequisites failed for " + prerequisites[i + 1]);
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
