using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light Weapon Attack Data", menuName = "Weapon Morph/Attack/Heavy")]
public class HeavyWeaponOutdatedAttackData : WeaponOutdatedAttackData
{
    public override OutdatedWeaponAttack CreateWeaponAttackInstance(GameObject owner, OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        return new HeavyOutdatedWeaponAttack(owner, ownerOutdatedMorph, ownerOutdatedDamageHandler, this, CreateOnHitEffectInstances(ownerOutdatedMorph, ownerOutdatedDamageHandler));
    }
}
