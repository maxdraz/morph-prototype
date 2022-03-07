using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFortitudeDamage : IDamageType
{
    public float FortitudeDamage { get; set; }

    public string StatusEffect { get; set; }

    public float Duration { get; set; }
}
