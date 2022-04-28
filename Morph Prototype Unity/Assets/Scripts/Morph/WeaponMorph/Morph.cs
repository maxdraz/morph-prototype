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


    public string GetEnumType() 
    {
        string value = morphType.ToString();
        Debug.Log(value);
        return value; 
    }
    
}
