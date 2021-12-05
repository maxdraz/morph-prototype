using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAcidDamage : IChemicalDamage
{
    public float AcidDamage { get; set; }
    public float AcidDOTDuration { get; set; }
    public float AcidPercentageBonusDamage { get; set; }
    public float AcidDOTModifier { get; set; }
}
