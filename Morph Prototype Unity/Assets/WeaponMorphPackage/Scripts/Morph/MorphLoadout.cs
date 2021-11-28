using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphLoadout : MonoBehaviour
{
    [SerializeField] private LimbWeaponMorphData limbWeaponMorphData;
    [SerializeField] private HeadWeaponMorphData headWeaponMorphData;
    [SerializeField] private TailWeaponMorphData tailWeaponMorphData;
    
    private LimbWeaponMorph limbWeaponMorph;
    private TailWeaponMorph tailWeaponMorph;
    private HeadWeaponMorph headWeaponMorph;

    public event Action<WeaponMorph> MorphLoadoutChanged;

    private void Start()
    {
        limbWeaponMorph = (LimbWeaponMorph)limbWeaponMorphData.CreateWeaponMorphInstance(gameObject);
        MorphLoadoutChanged?.Invoke(limbWeaponMorph);
    }

    public T GetWeaponMorph<T>() where T: WeaponMorph
    {
        if (typeof(T).IsAssignableFrom(typeof(LimbWeaponMorph)))
        {
            return limbWeaponMorph as T;
        } else if(typeof(T).IsAssignableFrom(typeof(TailWeaponMorph)))
        {
            return tailWeaponMorph as T;
        }
        else if(typeof(T).IsAssignableFrom(typeof(HeadWeaponMorph)))
        {
            return headWeaponMorph as T;
        }

        return null;
    }
    
    
}
