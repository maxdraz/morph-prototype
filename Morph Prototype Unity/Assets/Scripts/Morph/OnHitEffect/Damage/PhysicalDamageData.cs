using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysicalDamageData : OnHitEffectData, IPhysicalDamage
{
    private float morphDamage;
    private float physicalDamageDealt;
    [SerializeField] private float strikeModifier;
    [SerializeField] private float weaponCritChance;

    public float MorphDamage
    {
        get => morphDamage;
        set => morphDamage = value;
    }

    public float StrikeModifier
    {
        get => strikeModifier;
        set => strikeModifier = value;
    }
    public float PhysicalDamageDealt
    {
        get => physicalDamageDealt;
        set => physicalDamageDealt = value;
    }
    public float WeaponCritChance
    {
        get => weaponCritChance;
        set => weaponCritChance = value;
    }

    public bool IsCrit { get; set; }

    public PhysicalDamageData(float strikeModifier = 2f, float morphDamage = 0, float physicalDamageDealt = 0, float weaponCritChance = 0, bool isCrit = false)
    {
        this.morphDamage = morphDamage;
        this.strikeModifier = strikeModifier;
        this.physicalDamageDealt = physicalDamageDealt;
        this.weaponCritChance = weaponCritChance;
        IsCrit = isCrit;
    }
    
    public override object Clone()
    {
        return new PhysicalDamageData(strikeModifier, morphDamage, physicalDamageDealt, weaponCritChance, IsCrit);
    }
}
