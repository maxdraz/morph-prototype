using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TailWeaponMorph : WeaponMorph
{
    public TailWeaponMorph(GameObject owner, DamageHandler ownerDamageHandler, WeaponMorphData data) 
        : base( owner,  ownerDamageHandler,  data)
    {
    }
}
