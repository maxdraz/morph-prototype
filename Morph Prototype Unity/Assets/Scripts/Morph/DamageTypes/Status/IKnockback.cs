using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockback : IDamageType
{
   public float KnockbackForce { get; set; }
}
