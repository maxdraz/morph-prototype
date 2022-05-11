using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MorphType
{
    None,
    Melee,
    Ranged,
    Fire,
    Ice,
    Electric,
    Poison,
    Acid,
    Stealth,
    Intimidation
};


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

