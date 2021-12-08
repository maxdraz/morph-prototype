using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutdatedDamageHandler))]
public class MorphLoadout : MonoBehaviour
{
    [SerializeField] private LimbWeaponMorph limbWeaponMorph;
    [SerializeField] private HeadWeaponMorph headWeaponMorph;
    [SerializeField] private TailWeaponMorph tailWeaponMorph;

    [SerializeField] private List<PassiveMorph> passiveMorphs;
    
    [HideInInspector] public LimbWeaponMorph LimbWeaponMorph;
    [HideInInspector] public HeadWeaponMorph HeadWeaponMorph;
    [HideInInspector] public TailWeaponMorph TailWeaponMorph;

    public event Action<Morph> MorphLoadoutChanged;

    private void Start()
    {
        AddMorphToLoadout(limbWeaponMorph);
        AddMorphToLoadout(headWeaponMorph);
        AddMorphToLoadout(tailWeaponMorph);

        for (int i = 0; i < passiveMorphs.Count; i++)
        {
            passiveMorphs[i] = UtilityFunctions.CopyComponent(passiveMorphs[i], gameObject);
        }

    }
    

    public void AddMorphToLoadout(Morph morphPrefab)
    {
        if(!morphPrefab) return;
        
        if (morphPrefab is LimbWeaponMorph limbMorph)
        {
            limbWeaponMorph = UtilityFunctions.CopyComponent(limbMorph, gameObject);
          
            if (limbWeaponMorph)
            {
                MorphLoadoutChanged?.Invoke(limbWeaponMorph);
            }
        } else if (morphPrefab is HeadWeaponMorph headMorph)
        {
            headWeaponMorph = UtilityFunctions.CopyComponent(headMorph, gameObject);
            if (headWeaponMorph)
            {
                MorphLoadoutChanged?.Invoke(headWeaponMorph);
            }
        }else if (morphPrefab is TailWeaponMorph tailMorph)
        {
            tailWeaponMorph = UtilityFunctions.CopyComponent(tailMorph, gameObject);
            if (tailWeaponMorph)
            {
                MorphLoadoutChanged?.Invoke(tailWeaponMorph);
            }
        }else if (morphPrefab is PassiveMorph passiveMorph)
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
    
}
