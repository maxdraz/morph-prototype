using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Head Weapon Morph Data", menuName = "Weapon Morph/Morph/Head")]
public class HeadOutdatedWeaponMorphData : OutdatedWeaponMorphData
{
    public override WeaponOutdatedMorph CreateWeaponMorphInstance(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data)
    {
        //TODO
        return new HeadWeaponOutdatedMorph( owner,  ownerOutdatedDamageHandler,  data);
    }
}
