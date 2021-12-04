using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILifestealDamage : IDamageType
{
    public float LifestealAmount { get; set; }
}
