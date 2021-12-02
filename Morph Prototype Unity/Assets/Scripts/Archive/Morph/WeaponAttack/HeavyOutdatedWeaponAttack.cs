using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeavyOutdatedWeaponAttack : OutdatedWeaponAttack
{
    public HeavyOutdatedWeaponAttack(GameObject owner, OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler, WeaponOutdatedAttackData weaponOutdatedAttackData, List<OutdatedOnHitEffect> baseOnHitEffects) 
        : base(owner, ownerOutdatedMorph, ownerOutdatedDamageHandler, weaponOutdatedAttackData, baseOnHitEffects)
    {
            
    }

}
