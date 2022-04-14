using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType
{
    Health,
    Defence,
    MeleeDamage
}

[Serializable] public struct StatPrerequisite 
{
        public StatType stat;
        public int value;

        public StatPrerequisite(StatType a, int b)
        {
            stat = a;
            value = b;
        }
}
