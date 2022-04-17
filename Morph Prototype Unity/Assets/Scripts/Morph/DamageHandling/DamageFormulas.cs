using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageFormulas
{
    public static float PhysicalDamage(float weaponMorphDamage, float meleeDamageStatModifier, float strikeModifier,
        float percentageBonusDamage, float flatBonusDamage, float globalCritChance, float weaponCritChance, float attackCritChance, out bool isCrit)
    {
        isCrit = false;
        weaponMorphDamage = weaponMorphDamage * (1 + (meleeDamageStatModifier + strikeModifier + percentageBonusDamage) + flatBonusDamage);

        float totalCritChance = globalCritChance + weaponCritChance + attackCritChance;


        if (totalCritChance > 0)
        {
            isCrit = RollCrit(totalCritChance);
            if(isCrit)
            {
                weaponMorphDamage = weaponMorphDamage * 2.5f;
            }
            Debug.Log("Crit:" + isCrit);
        }

        float damageOut = weaponMorphDamage;
        return damageOut;
    }

    public static float PhysicalDamageResist(float damageIn, bool isPiercing, float toughnessModifier, float acidifiedDebuff, float percentageDmgReduction,
        bool hasArmour, float flatDamageReduction)
    {
        toughnessModifier = Mathf.Max(0, toughnessModifier - acidifiedDebuff);

        float damageOut = damageIn * (1 - (toughnessModifier + percentageDmgReduction));

        if (!isPiercing && hasArmour)
            damageOut *= 0.8f;

        damageOut -= flatDamageReduction;

        return damageOut;
    }

    public static float ElementalDamage(float baseDamage, float elementalDamageStatModifier, float percentageBonusDamage,
        float flatBonusDamage)
    {
        return (baseDamage * (1 + (elementalDamageStatModifier + percentageBonusDamage)) + flatBonusDamage);
    }

    public static float ElementalDamageResist(float damageIn, float elementalResistModifier, float percentageDmgReduction,
        float flatDamageReduction)
    {
        return (damageIn * (1 - (elementalResistModifier + percentageDmgReduction))) - flatDamageReduction;
    }

    public static float FortitudeDamage(float baseDamage, float percentageFortitudeBonusDamage, float flatBonusFortitudeDamage)
    {
        return (baseDamage * (1 + percentageFortitudeBonusDamage)) + flatBonusFortitudeDamage;
    }

    public static float FortitudeDamageResist(float damageIn, float percentageFortitudeDmgReduction, float flatFortitudeDamageReduction)
    {
        return (damageIn * (1 - percentageFortitudeDmgReduction)) - flatFortitudeDamageReduction;
    }

    public static float PoisonDamage()
    {
        return 0;
    }

    static bool RollCrit(float critChance)
    {
        Debug.Log("Roll for crit with totalcritchance of: " + critChance);

        if (Random.Range(0, 100) <= critChance)
        {
            Debug.Log("Critical Hit!!!");
            return true;
        }
        else 
        {
            Debug.Log("critical hit roll failed...");
            return false;
        }
    }
}
