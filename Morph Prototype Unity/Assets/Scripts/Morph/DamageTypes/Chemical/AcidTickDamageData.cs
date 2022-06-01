using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTickDamageData : IAcidTickDamage
{
    public AcidTickDamageData(float acidDamage = 1, float acidDOTDuration = 5f, float acidPercentageBonusDamage = 1f,
        float acidDotModifier = 1f)
    {
        this.AcidDamage = acidDamage;
        this.AcidDOTDuration = acidDOTDuration;
        this.AcidPercentageBonusDamage = acidPercentageBonusDamage;
        this.AcidDOTModifier = acidDotModifier;
    }

    public object Clone()
    {
        return new AcidTickDamageData(AcidDamage, AcidDOTDuration, AcidPercentageBonusDamage, AcidDOTModifier);
    }

    public float AcidDamage { get; set; }

    public float AcidDOTDuration { get; set; }

    public float AcidPercentageBonusDamage { get; set; }

    public float AcidDOTModifier { get; set; }
    public AttackType AttackType { get; set; }
}
