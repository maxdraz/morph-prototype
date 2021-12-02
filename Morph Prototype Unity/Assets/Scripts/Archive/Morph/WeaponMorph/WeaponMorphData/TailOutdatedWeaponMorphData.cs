using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tail Weapon Morph Data", menuName = "Weapon Morph/Morph/Tail")]
public class TailOutdatedWeaponMorphData : OutdatedWeaponMorphData
{
    public override WeaponOutdatedMorph CreateWeaponMorphInstance(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data)
    {
        //TODO 
        return new TailWeaponOutdatedMorph( owner,  ownerOutdatedDamageHandler,  data);
    }
}
