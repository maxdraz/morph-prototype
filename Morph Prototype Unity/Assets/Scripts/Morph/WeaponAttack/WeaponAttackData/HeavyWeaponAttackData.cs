using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light Weapon Attack Data", menuName = "Weapon Morph/Attack/Heavy")]
public class HeavyWeaponAttackData : WeaponAttackData
{
    public override WeaponAttack CreateWeaponAttackInstance(GameObject owner, Morph OwnerMorph, DamageHandler ownerDamageHandler)
    {
        return new HeavyWeaponAttack(owner, OwnerMorph, ownerDamageHandler, this, CreateOnHitEffectInstances(OwnerMorph, ownerDamageHandler));
    }
}
