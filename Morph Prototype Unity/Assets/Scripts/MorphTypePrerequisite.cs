using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MorphType
{
  Melee,
  Ranged,
  Fire,
  Ice,
  Electric,
  Poison,
  Acid
}


[Serializable] public struct MorphTypePrerequisite
{
    public MorphType type;
    public int amount;

    public MorphTypePrerequisite(MorphType a, int b)
    {
        type = a;
        amount = b;
    }
}

