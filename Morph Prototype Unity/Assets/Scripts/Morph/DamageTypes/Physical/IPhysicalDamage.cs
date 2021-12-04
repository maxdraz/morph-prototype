using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysicalDamage : IDamageType
{
    public float MorphDamage { get; set; }
    public float StrikeModifier { get; set; }
    public float PhysicalDamageDealt { get; set; }
}
