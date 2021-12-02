using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeadWeaponOutdatedMorph : WeaponOutdatedMorph
{
    public HeadWeaponOutdatedMorph(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data) 
        : base( owner,  ownerOutdatedDamageHandler,  data)
    {
    }
}
