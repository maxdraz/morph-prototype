using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeadWeaponMorph : WeaponMorph
{
    public HeadWeaponMorph(GameObject owner, DamageHandler ownerDamageHandler, WeaponMorphData data) 
        : base( owner,  ownerDamageHandler,  data)
    {
    }
}
