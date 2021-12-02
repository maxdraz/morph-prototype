using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light Weapon Attack Data", menuName = "Weapon Morph/Attack/Light")]
public class LightWeaponOutdatedAttackData : WeaponOutdatedAttackData
{
    public override OutdatedWeaponAttack CreateWeaponAttackInstance(GameObject owner, OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
         return new LightOutdatedWeaponAttack(owner, ownerOutdatedMorph, ownerOutdatedDamageHandler, this, CreateOnHitEffectInstances(ownerOutdatedMorph, ownerOutdatedDamageHandler));
    }
}
