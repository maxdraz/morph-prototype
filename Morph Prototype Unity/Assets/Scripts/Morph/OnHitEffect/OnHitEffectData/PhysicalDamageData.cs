using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysicalDamageData : OnHitEffectData, IPhysicalDamage
{
    private float morphDamage;
    private float physicalDamageDealt;
    [SerializeField] private float strikeModifier;
    
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

    public PhysicalDamageData(float strikeModifier = 2f, float morphDamage = 0, float physicalDamageDealt = 0)
    {
        this.morphDamage = morphDamage;
        this.strikeModifier = strikeModifier;
        this.physicalDamageDealt = physicalDamageDealt;
    }
    
    public override object Clone()
    {
        return new PhysicalDamageData(strikeModifier, morphDamage, physicalDamageDealt);
    }
}
