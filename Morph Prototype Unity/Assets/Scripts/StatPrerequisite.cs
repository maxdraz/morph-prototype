using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public struct StatPrerequisite 
{
        public string stat;
        public int value;

        public StatPrerequisite(string a, int b)
        {
            stat = a;
            value = b;
        }
}
