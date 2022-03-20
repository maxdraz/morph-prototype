using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public struct Prerequisite 
{
        public string stat;
        public int value;

        public Prerequisite(string a, int b)
        {
            stat = a;
            value = b;
        }
}
