using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Limb Weapon Morph Data", menuName = "Weapon Morph/Morph/Limb")]
public  class LimbWeaponMorphData : WeaponMorphData
{
    public override WeaponMorph CreateWeaponMorphInstance(GameObject owner)
    {
        return new LimbWeaponMorph(owner, this);
    }
}
