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

    public event Action<Morph> MorphLoadoutChanged;

    private void Awake()
    {
       
    }

    private void Start()
    {
        AddMorphToLoadout(limbWeaponMorph);

        for (int i = 0; i < passiveMorphs.Count; i++)
        {
            passiveMorphs[i] = UtilityFunctions.CopyComponent(passiveMorphs[i], gameObject);
        }
            
        
    }
    

    public void AddMorphToLoadout(WeaponMorph morphPrefab)
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
        }
    }
    
}
