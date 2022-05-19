using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable] public struct StatValue 
{
        public StatType stat;
        public int value;

        public StatValue(StatType a, int b)
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
