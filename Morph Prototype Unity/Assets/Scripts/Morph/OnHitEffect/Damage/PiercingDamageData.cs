using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PiercingDamageData : OnHitEffectData, IPiercingDamage
{
    private float morphDamage;
    [SerializeField] private float strikeModifier;
    private float physicalDamageDealt;
    
    public float MorphDamage { 
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

    public PiercingDamageData(float morphDamage =0, float strikeModifier=1, float physicalDamageDealt = 0)
    {
        this.morphDamage = morphDamage;
        this.strikeModifier = strikeModifier;
        this.physicalDamageDealt = physicalDamageDealt;
    }
    
    
    
    public override object Clone()
    {
        return new PiercingDamageData(morphDamage, strikeModifier, physicalDamageDealt);
    }

    
}
