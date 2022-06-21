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
    protected MorphLoadout morphLoadout;

    public MorphType[] morphTypes;
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
        StopAllCoroutines();
    }

    protected virtual void Update()
    {
    }

    //private void UnlockSecondary(string name) 
    //{
    //    Debug.Log(this.name + " is trying to: " + "unlock" + name);
    //    BroadcastMessage("UnlockSecondary", name);
    //}

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
    
    protected bool CheckSecondaryPrerequisites(MorphLoadout loadout, Stats stats) // TODO make it return bool, don't unlock anything here
    {
        string[] secondariesToUnlock = new string[prerequisites.Count];
        bool unlockSecondary = false;

        for (int i = 0; i < prerequisites.Count; i++)
        {
            if (prerequisites[i].CheckSecondaryPrerequisites(loadout, stats) == true) 
            {
                Debug.Log("SecondaryPrerequisites check for " + prerequisites[i] + " returned true");
                secondariesToUnlock[i] = prerequisites[i].name; // this name can be used to find the prerequisite that passed its check
                unlockSecondary = true;
            }

            else
            {
                Debug.Log("SecondaryPrerequisites check for " + prerequisites[i] + " returned false");
                unlockSecondary = false;
            }

            
        }

        //for (int i = 0; i < secondariesToUnlock.Length; i++) 
        //{
        //    if (secondariesToUnlock[i] != null)
        //    {
        //        Debug.Log("CheckSecondaryPrerequisites succeeded for " + secondariesToUnlock[i]);
        //        UnlockSecondary(secondariesToUnlock[i]);
        //    }
        //}

        if (unlockSecondary)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public string GetMorphType(int T)
    {
        return morphTypes[T].ToString();
    }

    public void Initialize()
    {
        damageHandler = GetComponent<DamageHandler>();
        stats = GetComponent<Stats>();
        morphLoadout = GetComponent<MorphLoadout>();
        

        GetComponentReferences();
        SubscribeEvents();
        OnEquip();

        initializationComplete = true;
    }

    protected virtual void GetComponentReferences()
    {
        
    }

    protected virtual void SubscribeEvents()
    {
        
    }
    
    protected virtual void UnsubscribeEvents()
    {
        
    }

    protected virtual void OnEquip()    // apply starting effects here
    {
        
    }
    
    protected virtual void OnUnequip()  // reverse effects
    {
        
    }
}
