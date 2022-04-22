using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType
{
    MeleeDamage,
    RangedDamage,
    ElementalDamage,
    ChemicalDamage,
    Fortitude,
    Toughness,
    Intimidation,
    Agility,
    Stealth,
    Perception,
    Intelligence
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

    //public string FindStatName() 
    //{
    //    string statName = StatType.GetName(typeof(StatType),stat);
    //
    //    return statName;
    //}
}
