using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrutalSpurs : LimbWeaponMorph
{
    private static int meleeDamagePrerequisite = 10;


    [SerializeField]
    //static Prerequisite[] StatPrerequisits;

    private void Start()
    {
        //WriteToPrerequisiteArray();
    }

    // public override bool CheckPrerequisites(MorphLoadout loadout)
    // {
    //     var stats = loadout.GetComponent<Stats>();
    //     var health = loadout.GetComponent<Health>();
    //     
    //     bool specificMorph = loadout.IsMorphEquipped<BrutalSpurs>();
    //
    //     return twoPoisonMorphs || specificMorph;
    // }

    // void WriteToPrerequisiteArray()
    // {
    //     statPrerequisits = new Prerequisite[StatPrerequisits.Length];
    //
    //     for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
    //     {
    //         statPrerequisits[i] = StatPrerequisits[i];
    //         Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
    //     }
    // }
}
