using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageFormulas
{
    public static float PhysicalDamage(float weaponMorphDamage, float meleeDamageStat, float currentAttackDamage, float bonusDamage, float flatBonusDamage)
    {
        return (weaponMorphDamage * (1 + (meleeDamageStat + currentAttackDamage + bonusDamage))) + flatBonusDamage;
    }
}
