using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutdatedDamageHandler))]
public class MorphLoadout : MonoBehaviour
{
    private LimbOutdatedWeaponMorphData limbOutdatedWeaponMorphData;
    private HeadOutdatedWeaponMorphData headOutdatedWeaponMorphData;
    private TailOutdatedWeaponMorphData tailOutdatedWeaponMorphData;

    [SerializeField] private LimbWeaponMorph limbWeaponMorph;
    [SerializeField] private HeadWeaponMorph headWeaponMorph;
    [SerializeField] private TailWeaponMorph tailWeaponMorph;
    
    private LimbWeaponOutdatedMorph _limbWeaponOutdatedMorph;
    private TailWeaponOutdatedMorph _tailWeaponOutdatedMorph;
    private HeadWeaponOutdatedMorph _headWeaponOutdatedMorph;
    private OutdatedDamageHandler _outdatedDamageHandler;

    public event Action<Morph> MorphLoadoutChanged;

    private void Awake()
    {
        _outdatedDamageHandler = GetComponent<OutdatedDamageHandler>();
    }

    private void Start()
    {
        AddMorphToLoadout(limbWeaponMorph);
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
