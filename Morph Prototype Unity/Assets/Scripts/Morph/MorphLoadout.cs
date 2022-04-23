using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageHandler))]
public class MorphLoadout : MonoBehaviour
{
    [SerializeField] private LimbWeaponMorph limbWeaponMorph;
    [SerializeField] private HeadWeaponMorph headWeaponMorph;
    [SerializeField] private TailWeaponMorph tailWeaponMorph;

    //Active morphs were originally held in an array, changed to list recently
    [SerializeField] private List<ActiveMorph> activeMorphs;
    [SerializeField] private List<PassiveMorph> passiveMorphs;

    [HideInInspector] public LimbWeaponMorph LimbWeaponMorph;
    [HideInInspector] public HeadWeaponMorph HeadWeaponMorph;
    [HideInInspector] public TailWeaponMorph TailWeaponMorph;

    public event Action<Morph> MorphLoadoutChanged;

    private void Start()
    {
        AddMorphsAtStart();

    }
    void AddMorphsAtStart()
    {
        AddMorphToLoadout(limbWeaponMorph);
        AddMorphToLoadout(headWeaponMorph);
        AddMorphToLoadout(tailWeaponMorph);

        for (int i = 0; i < activeMorphs.Count; i++)
        {
            var currentActiveMorph = activeMorphs[i];
            if (currentActiveMorph)
            {
                activeMorphs[i] = UtilityFunctions.CopyComponent(activeMorphs[i], gameObject);
            }
        }

        for (int i = 0; i < passiveMorphs.Count; i++)
        {
            var currentPassiveMorph = passiveMorphs[i];
            if (currentPassiveMorph)
                passiveMorphs[i] = UtilityFunctions.CopyComponent(passiveMorphs[i], gameObject);
        }
    }



     public void AddMorphToLoadoutAtRuntime(Morph morphPrefab) 
     {
         if (!morphPrefab) return;

        if (morphPrefab.CheckPrerequisites(GetComponent<MorphLoadout>(), GetComponent<Stats>(), morphPrefab) == true)
        {


            if (morphPrefab is LimbWeaponMorph limbMorph)
            {
                limbWeaponMorph = UtilityFunctions.CopyComponent(limbMorph, gameObject);

                if (limbWeaponMorph)
                {
                    MorphLoadoutChanged?.Invoke(limbWeaponMorph);
                }
            }
            else if (morphPrefab is HeadWeaponMorph headMorph)
            {
                headWeaponMorph = UtilityFunctions.CopyComponent(headMorph, gameObject);
                if (headWeaponMorph)
                {
                    MorphLoadoutChanged?.Invoke(headWeaponMorph);
                }
            }
            else if (morphPrefab is TailWeaponMorph tailMorph)
            {
                tailWeaponMorph = UtilityFunctions.CopyComponent(tailMorph, gameObject);
                if (tailWeaponMorph)
                {
                    MorphLoadoutChanged?.Invoke(tailWeaponMorph);
                }
            }
            else if (morphPrefab is PassiveMorph passiveMorph)
            {
                var morph = UtilityFunctions.CopyComponent(passiveMorph, gameObject);
                if (morph)
                {
                    passiveMorphs.Add(morph);
                    MorphLoadoutChanged?.Invoke(morph);
                }
            }
            else if (morphPrefab is ActiveMorph activeMorph)
            {
                var morph = UtilityFunctions.CopyComponent(activeMorph, gameObject);
                if (morph)
                {
                    activeMorphs.Add(morph);
                    MorphLoadoutChanged?.Invoke(morph);
                }
            }
        }
    }

    public void AddMorphToLoadout(Morph morphPrefab)
    {


        if (morphPrefab is LimbWeaponMorph limbMorph)
        {
            limbWeaponMorph = UtilityFunctions.CopyComponent(limbMorph, gameObject);

            if (limbWeaponMorph)
            {
                MorphLoadoutChanged?.Invoke(limbWeaponMorph);
            }
        }
        else if (morphPrefab is HeadWeaponMorph headMorph)
        {
            headWeaponMorph = UtilityFunctions.CopyComponent(headMorph, gameObject);
            if (headWeaponMorph)
            {
                MorphLoadoutChanged?.Invoke(headWeaponMorph);
            }
        }
        else if (morphPrefab is TailWeaponMorph tailMorph)
        {
            tailWeaponMorph = UtilityFunctions.CopyComponent(tailMorph, gameObject);
            if (tailWeaponMorph)
            {
                MorphLoadoutChanged?.Invoke(tailWeaponMorph);
            }
        }
        else if (morphPrefab is PassiveMorph passiveMorph)
        {
            var morph = UtilityFunctions.CopyComponent(passiveMorph, gameObject);
            if (morph)
            {
                passiveMorphs.Add(morph);
                MorphLoadoutChanged?.Invoke(morph);
            }
        }
    }

    public void RemoveLimbWeaponMorph()
    {
        Destroy(limbWeaponMorph);
        // limbWeaponMorph = null;

        // MorphLoadoutChanged?.Invoke(limbWeaponMorph);
    }

    public ActiveMorph GetActiveMorph(int index)
    {
        if (index >= activeMorphs.Count) return null;

        return activeMorphs[index];
    }

    

    public bool GetMorphsByType(string typeToSearchFor, int amountToFind)
    {
        int amountFound = 0;
        
        foreach (ActiveMorph activeMorph in activeMorphs)
        {
            
            if (Enum.GetName(typeof(MorphType), activeMorph).Equals(typeToSearchFor))
            {
                amountFound++;
            }
        }
        foreach (PassiveMorph passiveMorph in passiveMorphs)
        {
            if (Enum.GetName(typeof(MorphType), passiveMorphs).Equals(typeToSearchFor))
            {
                amountFound++;
            }
        }
        
        if (Enum.GetName(typeof(MorphType), LimbWeaponMorph).Equals(typeToSearchFor))
        {
            amountFound++;
        }
        
        
        if (Enum.GetName(typeof(MorphType), TailWeaponMorph).Equals(typeToSearchFor))
        {
            amountFound++;
        }
        
        
        if (Enum.GetName(typeof(MorphType), HeadWeaponMorph).Equals(typeToSearchFor))
        {
            amountFound++;
        }
        
        if (amountFound == amountToFind) 
        {
        return true;
        }
        else 
        {
            return false;
        }
    }

    public bool GetPrerequisiteMorphByName(string morphName)
    {
        bool morphFound = false;

        foreach (ActiveMorph activeMorph in activeMorphs)
        {
            if (activeMorph.name.Equals(morphName))
            {
                morphFound = true;
            }
        }
        foreach (PassiveMorph passiveMorph in passiveMorphs)
        {
            if (passiveMorph.name.Equals(morphName))
            {
                morphFound = true;
            }
        }
        
        if (LimbWeaponMorph.name.Equals(morphName))
        {
            morphFound = true;
        }
        
        
        if (tailWeaponMorph.name.Equals(morphName))
        {
            morphFound = true;
        }
        
        
        if (headWeaponMorph.name.Equals(morphName))
        {
            morphFound = true;
        }

        if (morphFound)
        {
            return true;
        }
        else
        { 
            return false; 
        }
    }

    public void RemoveMorphFromLoadout(Morph morphObj)
    {
        if(!morphObj) return;
        
        if (morphObj is LimbWeaponMorph limbMorph)
        {
           Destroy(limbWeaponMorph);
           limbWeaponMorph = null;
           
           MorphLoadoutChanged?.Invoke(limbWeaponMorph);
           
        } else if (morphObj is HeadWeaponMorph headMorph)
        {
            Destroy(headWeaponMorph);
            headWeaponMorph = null;
            
            MorphLoadoutChanged?.Invoke(headWeaponMorph);
        }else if (morphObj is TailWeaponMorph tailMorph)
        {
            Destroy(tailWeaponMorph);
            tailWeaponMorph = null;
           
            MorphLoadoutChanged?.Invoke(tailWeaponMorph);
            
        }else if (morphObj is PassiveMorph passiveMorph)
        {
            for (int i = 0; i < passiveMorphs.Count; i++)
            {
                var currentPassiveMorph = passiveMorphs[i];
                if (currentPassiveMorph == passiveMorph)
                {
                    passiveMorphs.RemoveAt(i--);
                    Destroy(currentPassiveMorph);
                }
            }
            
            MorphLoadoutChanged?.Invoke(null);
            
        }
    }

    public bool IsMorphEquipped<T>() where T : Morph
    {
        if (limbWeaponMorph is T)
            return true;

        if (headWeaponMorph is T)
            return true;
        
        if (tailWeaponMorph is T)
            return true;

        foreach (var activeMorph in activeMorphs)
        {
            if (activeMorph is T)
                return true;
        }

        foreach (var passiveMorph in passiveMorphs)
        {
            if (passiveMorph is T)
                return true;
        }
        
        return false;
    }
    
}
