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

    protected DamageHandler damageHandler;
    protected Stats stats;

    public MorphType morphType;
    [SerializeField] private List<PrerequisiteData> prerequisites;

    //This was to be used with activating secondaries as a precedural name for secondaryunlock bools
    //public Dictionary<string, bool> boolHolder = new Dictionary<string, bool>();

    public StatValue[] statsToModify;
    protected bool initializationComplete;

    protected void OnEnable()
    {
        if(!initializationComplete) return;
        UnsubscribeEvents();
        SubscribeEvents();
        OnEquip();
    }
    
    protected void OnDisable()
    {
        OnUnequip();
        UnsubscribeEvents();
    }

    protected virtual void Update()
    {
    }

    private void UnlockSecondary(string name) 
    {
        Debug.Log(this.name + " is trying to: " + "unlock" + name);
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
    
    private void CheckSecondaryPrerequisites(MorphLoadout loadout, Stats stats)
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
        return morphType.ToString();
    }

    public void Initialize()
    {
        damageHandler = GetComponent<DamageHandler>();
        stats = GetComponent<Stats>();
        
        GetComponentReferences();
        SubscribeEvents();
        OnEquip();

        initializationComplete = true;
    }

    protected virtual void GetComponentReferences()
    {
        // get all necessary components
    }

    protected virtual void SubscribeEvents()
    {
        
    }
    
    protected virtual void UnsubscribeEvents()
    {
        
    }

    protected virtual void OnEquip()
    {
        // apply effects that happen immediately
    }
    
    protected virtual void OnUnequip()
    {
        // reverse effects
    }
}
