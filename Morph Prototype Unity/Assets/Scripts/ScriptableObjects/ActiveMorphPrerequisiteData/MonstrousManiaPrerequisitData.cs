using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monstrous Mania Prerequisite Data", menuName = "Active Morph Prerequisite Data/Monstrous Mania")]
public class MonstrousManiaPrerequisiteData: ScriptableObject
{
    //Monstrous Mania prerequisits
    public StatPrerequisite[] MonstrousManiaStatPrerequisites;
    public MorphTypePrerequisite[] MonstrousManiaTypePrerequisites;
    public Morph[] MonstrousManiaMorphPrerequisites;
}




