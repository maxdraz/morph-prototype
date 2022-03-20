using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBeak : HeadWeaponMorph
{
    private static int meleeDamagePrerequisite = 10;



    public Prerequisite[] StatPrerequisits;

    private void Start()
    {
        WriteToPrerequisiteArray();
    }

    void WriteToPrerequisiteArray()
    {
        statPrerequisits = new Prerequisite[StatPrerequisits.Length];

        for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
        {
            statPrerequisits[i] = StatPrerequisits[i];
            Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
        }
    }
}
