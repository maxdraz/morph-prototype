using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageType : ICloneable
{
    public AttackType AttackType { get; set; }
}
