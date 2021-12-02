using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Limb Weapon Morph Data", menuName = "Weapon Morph/Morph/Limb")]
public  class LimbOutdatedWeaponMorphData : OutdatedWeaponMorphData
{
    public override WeaponOutdatedMorph CreateWeaponMorphInstance(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data)
    {
        return new LimbWeaponOutdatedMorph( owner,  ownerOutdatedDamageHandler,  data);
    }
}
