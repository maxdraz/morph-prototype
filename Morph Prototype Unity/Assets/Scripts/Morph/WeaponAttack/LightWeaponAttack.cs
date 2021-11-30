using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightWeaponAttack: WeaponAttack
{
    public LightWeaponAttack(GameObject owner,Morph ownerMorph, DamageHandler ownerDamageHandler, WeaponAttackData weaponAttackData, List<OnHitEffect> baseOnHitEffects) 
        : base(owner, ownerMorph, ownerDamageHandler,weaponAttackData, baseOnHitEffects)
    {
    }
}
