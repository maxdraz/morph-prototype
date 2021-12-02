using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TailWeaponOutdatedMorph : WeaponOutdatedMorph
{
    public TailWeaponOutdatedMorph(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data) 
        : base( owner,  ownerOutdatedDamageHandler,  data)
    {
    }
}
