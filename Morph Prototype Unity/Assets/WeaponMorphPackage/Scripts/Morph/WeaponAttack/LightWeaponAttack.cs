using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightWeaponAttack: WeaponAttack
{
    public LightWeaponAttack(GameObject owner, WeaponAttackData weaponAttackData, List<OnHitEffect> baseOnHitEffects) 
        : base(owner,weaponAttackData, baseOnHitEffects)
    {
    }
}
