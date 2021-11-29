using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Head Weapon Morph Data", menuName = "Weapon Morph/Morph/Head")]
public class HeadWeaponMorphData : WeaponMorphData
{
    public override WeaponMorph CreateWeaponMorphInstance(GameObject owner)
    {
        //TODO
        return new HeadWeaponMorph(owner, this);
    }
}
