using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutdatedDamageHandler))]
public class MorphLoadout : MonoBehaviour
{
    [SerializeField] private LimbOutdatedWeaponMorphData limbOutdatedWeaponMorphData;
    [SerializeField] private HeadOutdatedWeaponMorphData headOutdatedWeaponMorphData;
    [SerializeField] private TailOutdatedWeaponMorphData tailOutdatedWeaponMorphData;
    
    private LimbWeaponOutdatedMorph _limbWeaponOutdatedMorph;
    private TailWeaponOutdatedMorph _tailWeaponOutdatedMorph;
    private HeadWeaponOutdatedMorph _headWeaponOutdatedMorph;
    private OutdatedDamageHandler _outdatedDamageHandler;

    public event Action<WeaponOutdatedMorph> MorphLoadoutChanged;

    private void Awake()
    {
        _outdatedDamageHandler = GetComponent<OutdatedDamageHandler>();
    }

    private void Start()
    {
        AddToLoadout(limbOutdatedWeaponMorphData);
        AddToLoadout(headOutdatedWeaponMorphData);
        AddToLoadout(tailOutdatedWeaponMorphData);
    }

    public T GetWeaponMorph<T>() where T: WeaponOutdatedMorph
    {
        if (typeof(T).IsAssignableFrom(typeof(LimbWeaponOutdatedMorph)))
        {
            return _limbWeaponOutdatedMorph as T;
        } else if(typeof(T).IsAssignableFrom(typeof(TailWeaponOutdatedMorph)))
        {
            return _tailWeaponOutdatedMorph as T;
        }
        else if(typeof(T).IsAssignableFrom(typeof(HeadWeaponOutdatedMorph)))
        {
            return _headWeaponOutdatedMorph as T;
        }

        return null;
    }

    public void AddToLoadout(OutdatedWeaponMorphData morphData)
    {
        if (morphData is LimbOutdatedWeaponMorphData limbData)
        {
            _limbWeaponOutdatedMorph =  (LimbWeaponOutdatedMorph) limbData.CreateWeaponMorphInstance(gameObject, _outdatedDamageHandler, limbData);
            MorphLoadoutChanged?.Invoke(_limbWeaponOutdatedMorph);
        } else if (morphData is HeadOutdatedWeaponMorphData headData)
        {
            _headWeaponOutdatedMorph = (HeadWeaponOutdatedMorph) headData.CreateWeaponMorphInstance(gameObject, _outdatedDamageHandler, headData);
            MorphLoadoutChanged?.Invoke(_headWeaponOutdatedMorph);
        }
        else if (morphData is TailOutdatedWeaponMorphData tailData)
        {
            _tailWeaponOutdatedMorph = (TailWeaponOutdatedMorph) tailData.CreateWeaponMorphInstance(gameObject, _outdatedDamageHandler, tailData);
            MorphLoadoutChanged?.Invoke(_tailWeaponOutdatedMorph);
        }
    }
    
    
}
