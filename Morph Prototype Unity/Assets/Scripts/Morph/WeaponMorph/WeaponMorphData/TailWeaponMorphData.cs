using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tail Weapon Morph Data", menuName = "Weapon Morph/Morph/Tail")]
public class TailWeaponMorphData : WeaponMorphData
{
    public override WeaponMorph CreateWeaponMorphInstance(GameObject owner)
    {
        //TODO 
        return new TailWeaponMorph(owner, this);
    }
}
