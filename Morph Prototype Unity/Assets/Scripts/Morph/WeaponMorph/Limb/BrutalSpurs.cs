using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrutalSpurs : LimbWeaponMorph
{
    private static int meleeDamagePrerequisite = 10;


    [SerializeField]
    private Prerequisite[] StatPrerequisits;

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
