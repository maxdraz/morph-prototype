using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeavyWeaponAttack : WeaponAttack
{
    public HeavyWeaponAttack(GameObject owner, WeaponAttackData weaponAttackData, List<OnHitEffect> baseOnHitEffects) 
        : base(owner, weaponAttackData, baseOnHitEffects)
    {
            
    }

}
