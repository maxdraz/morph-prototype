using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILightningDamage : IElementalDamage
{
    public float LightningDamage { get; set; }
}
