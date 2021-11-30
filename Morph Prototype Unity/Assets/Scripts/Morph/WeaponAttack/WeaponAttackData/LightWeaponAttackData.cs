using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light Weapon Attack Data", menuName = "Weapon Morph/Attack/Light")]
public class LightWeaponAttackData : WeaponAttackData
{
    public override WeaponAttack CreateWeaponAttackInstance(GameObject owner, Morph ownerMorph, DamageHandler ownerDamageHandler)
    {
         return new LightWeaponAttack(owner, ownerMorph, ownerDamageHandler, this, CreateOnHitEffectInstances(ownerMorph, ownerDamageHandler));
    }
}
