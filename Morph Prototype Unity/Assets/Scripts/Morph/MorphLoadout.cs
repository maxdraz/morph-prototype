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

    [SerializeField] private List<MorphType> morphTypesAttached;

    [HideInInspector] public LimbWeaponMorph LimbWeaponMorph;
    [HideInInspector] public HeadWeaponMorph HeadWeaponMorph;
    [HideInInspector] public TailWeaponMorph TailWeaponMorph;

    public event Action<Morph> MorphLoadoutChanged;

    private void Start()
    {
        AddMorphsAtStart();

        CountAllMorphsByType();
    }
    void AddMorphsAtStart()
    {
        if (limbWeaponMorph != null)
        {

            AddMorphToLoadout(limbWeaponMorph);

            if (limbWeaponMorph.GetMorphType() != "None")
            {
                MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), limbWeaponMorph.GetMorphType());
                morphTypesAttached.Add(morphType);
            }
        }

        if (headWeaponMorph != null) 
        {
            AddMorphToLoadout(headWeaponMorph);

            if (headWeaponMorph.GetMorphType() != "None")
            {
                MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), headWeaponMorph.GetMorphType());
                morphTypesAttached.Add(morphType);
            }
        }

        if (tailWeaponMorph != null) 
        {
            AddMorphToLoadout(tailWeaponMorph);

            if (tailWeaponMorph.GetMorphType() != "None")
            {
                MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), tailWeaponMorph.GetMorphType());
                morphTypesAttached.Add(morphType);
            }
        }

        for (int i = 0; i < activeMorphs.Count; i++)
        {
            var currentActiveMorph = activeMorphs[i];
            if (currentActiveMorph)
            {
                activeMorphs[i] = UtilityFunctions.CopyComponent(activeMorphs[i], gameObject);

                if (activeMorphs[i].GetMorphType() != "None")
                {
                    MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), activeMorphs[i].GetMorphType());
                    morphTypesAttached.Add(morphType);
                }
            }
        }

        for (int i = 0; i < passiveMorphs.Count; i++)
        {
            var currentPassiveMorph = passiveMorphs[i];
            if (currentPassiveMorph)
                passiveMorphs[i] = UtilityFunctions.CopyComponent(passiveMorphs[i], gameObject);
            //Debug.Log("Added " + passiveMorphs[i].GetType().Name);

            if (passiveMorphs[i].GetMorphType() != "None")
            {
                MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), passiveMorphs[i].GetMorphType());
                morphTypesAttached.Add(morphType);
            }
        }
    }

    private void CountAllMorphsByType() 
    {

        foreach (ActiveMorph activeMorph in activeMorphs)
        {
            if (activeMorph.GetMorphType() != "None")
            {
                //morphTypesAttached.Add(activeMorph.GetEnumType());
            }
        }
        
        foreach (PassiveMorph passiveMorph in passiveMorphs)
        {
            if (passiveMorph.GetMorphType() != "None")
            {
                //morphTypesAttached.Add(passiveMorph.GetEnumType());
            }
        }

        if (limbWeaponMorph != null)
        {
            if (limbWeaponMorph.GetMorphType() != "None")
            {
                //morphTypesAttached.Add(limbWeaponMorph.GetEnumType());
            }
        }

        if (headWeaponMorph != null)
        {
            if (headWeaponMorph.GetMorphType() != "None")
            {
                //morphTypesAttached.Add(headWeaponMorph.GetEnumType());
            }
        }

        if (tailWeaponMorph != null)
        {
            if (tailWeaponMorph.GetMorphType() != "None")
            {
                //morphTypesAttached.Add(tailWeaponMorph.GetEnumType());
            }
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
                if (limbWeaponMorph.GetMorphType() != "None") 
                {
                    MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), limbWeaponMorph.GetMorphType());
                    morphTypesAttached.Add(morphType);
                }
            }

            else if (morphPrefab is HeadWeaponMorph headMorph)
            {
                headWeaponMorph = UtilityFunctions.CopyComponent(headMorph, gameObject);
                if (headWeaponMorph)
                {
                    MorphLoadoutChanged?.Invoke(headWeaponMorph);
                }
                if (headWeaponMorph.GetMorphType() != "None")
                {
                    MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), headWeaponMorph.GetMorphType());
                    morphTypesAttached.Add(morphType);
                }
            }

            else if (morphPrefab is TailWeaponMorph tailMorph)
            {
                tailWeaponMorph = UtilityFunctions.CopyComponent(tailMorph, gameObject);
                if (tailWeaponMorph)
                {
                    MorphLoadoutChanged?.Invoke(tailWeaponMorph);
                }
                if (tailWeaponMorph.GetMorphType() != "None")
                {
                    MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), tailWeaponMorph.GetMorphType());
                    morphTypesAttached.Add(morphType);
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
                if (passiveMorph.GetMorphType() != "None")
                {
                    MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), passiveMorph.GetMorphType());
                    morphTypesAttached.Add(morphType);
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
                if (activeMorph.GetMorphType() != "None")
                {
                    MorphType morphType = (MorphType)Enum.Parse(typeof(MorphType), activeMorph.GetMorphType());
                    morphTypesAttached.Add(morphType);
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

    

    public int GetMorphTypeAmount(MorphType typeToSearchFor)
    {
        int amountFound = 0;

        Debug.Log("GetMorphTypeAmount (in MorphLoadout) is looking for morphs of type: " + typeToSearchFor);
        //For use with morphTypesAttached and CountAllMorphsByType()
        //This is the function which should be used to check for morph type amounts when determining: elemental status effect bar gain, or any other extra damage based on types attached
        foreach (MorphType type in morphTypesAttached)
        {
            if (type == typeToSearchFor)
            {
                amountFound++;
            }
        }

        Debug.Log("GetMorphTypeAmount (in MorphLoadout) found:" + amountFound + " of type: " + typeToSearchFor);
        return amountFound;
    }

    public bool GetPrerequisiteMorphByName(string morphName)
    {
        bool morphFound = false;
        Debug.Log("Loadout is looking for " + morphName);

        foreach (ActiveMorph activeMorph in activeMorphs)
        {
            if (activeMorph.GetType().Name == morphName)
            {
                Debug.Log("Loadout found " + activeMorph.GetType().Name);
                morphFound = true;
            }
        }
        foreach (PassiveMorph passiveMorph in passiveMorphs)
        {
            if (passiveMorph.GetType().Name == morphName)
            {
                Debug.Log("Loadout found " + passiveMorph.GetType().Name);
                morphFound = true;
            }
        }

        if (limbWeaponMorph != null) 
        {
            if (LimbWeaponMorph.GetType().Name == morphName)
            {
                Debug.Log("Loadout found " + limbWeaponMorph.GetType().Name);
                morphFound = true;
            }
        }


        if (tailWeaponMorph != null)
        {
            if (tailWeaponMorph.GetType().Name == morphName)
            {
                Debug.Log("Loadout found " + tailWeaponMorph.GetType().Name);
                morphFound = true;
            }
        }

        if (headWeaponMorph != null)
        {
            if (headWeaponMorph.GetType().Name == morphName)
            {
                Debug.Log("Loadout found " + headWeaponMorph.GetType().Name);
                morphFound = true;
            }
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
