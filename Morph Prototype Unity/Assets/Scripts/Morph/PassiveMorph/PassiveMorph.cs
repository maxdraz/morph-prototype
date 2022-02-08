using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMorph : Morph
{
    [SerializeField]public struct Prerequisite
    {
        string stat;
        int value;

        public Prerequisite(string a, int b)
        {
            stat = a;
            value = b;


        }
    }
}
