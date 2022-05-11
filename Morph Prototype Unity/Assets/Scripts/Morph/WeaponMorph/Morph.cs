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

    private string type;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    

    public virtual bool CheckPrerequisites(MorphLoadout loadout, Stats stats, Morph morphPrefab)
    {
        if (prerequisites == null || prerequisites.Count <= 0) return true;

        bool prerequisitesMet = false;
        foreach (var prerequisiteData in prerequisites)
        {
            prerequisitesMet = prerequisiteData.CheckPrerequisites(loadout, stats, morphPrefab);
        }
        
        return prerequisitesMet;
    }


    

    public string GetMorphType() 
    {
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

        return type;
    }
}
