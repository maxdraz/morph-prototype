using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysicalDamageData : OnHitEffectData, IPhysicalDamage
{
    private float morphDamage;
    [SerializeField] private float strikeModifier;

    public PhysicalDamageData(float strikeModifier = 2f, float morphDamage = 0)
    {
        this.morphDamage = morphDamage;
        this.strikeModifier = strikeModifier;
    }
    
    public override object Clone()
    {
        return new PhysicalDamageData(strikeModifier, morphDamage);
    }

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
}
