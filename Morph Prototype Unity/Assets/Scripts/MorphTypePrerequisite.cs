using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable] public struct MorphTypePrerequisite
{
    public string type;
    public int amount;

    public MorphTypePrerequisite(string a, int b)
    {
        type = a;
        amount = b;
    }
}

