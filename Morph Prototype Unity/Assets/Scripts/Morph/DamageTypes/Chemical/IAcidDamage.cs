using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAcidDamage : IChemicalDamage
{
    public float AcidDamage { get; set; }
}