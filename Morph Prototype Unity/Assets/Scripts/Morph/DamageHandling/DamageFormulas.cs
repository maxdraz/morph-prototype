using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageFormulas
{
    public static float PhysicalDamage(float weaponMorphDamage, float meleeDamageStatModifier, float strikeModifier, 
        float percentageBonusDamage, float flatBonusDamage)
    {
        return (weaponMorphDamage * (1 + (meleeDamageStatModifier + strikeModifier + percentageBonusDamage))) + flatBonusDamage;
    }

    public static float ElementalDamage(float baseDamage, float elementalDamageStatModifier, float percentageBonusDamage, 
        float flatBonusDamage)
    {
        return (baseDamage * (1 + (elementalDamageStatModifier + percentageBonusDamage)) + flatBonusDamage);
    }
    
    public static float PhysicalDamageResist(float damageIn, bool isPiercing, float toughnessModifier, float percentageDmgReduction, 
        bool hasArmour, float flatDamageReduction)
    {
        float damageOut = damageIn * (1 - (toughnessModifier + percentageDmgReduction));
        
        if (!isPiercing && hasArmour)
            damageOut *= 0.8f;
        
        damageOut -= flatDamageReduction;

        return damageOut;
    }
    
    public static float ElementalDamageResist(float damageIn, float elementalResistModifier, float percentageDmgReduction, 
        float flatDamageReduction)
    {
        return (damageIn * (1 - (elementalResistModifier + percentageDmgReduction))) - flatDamageReduction;
    }
}
