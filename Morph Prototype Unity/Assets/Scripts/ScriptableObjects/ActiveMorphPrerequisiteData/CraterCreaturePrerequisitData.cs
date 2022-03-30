using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crater Creature Prerequisite Data", menuName = "Morph Prerequisite Data/Crater Creature")]
public class CraterCreaturePrerequisiteData: ScriptableObject
{
    //Crater Creature Barrage prerequisites
    public StatPrerequisite[] CraterCreatureStatPrerequisites;
    public MorphTypePrerequisite[] CraterCreatureTypePrerequisites;
    public Morph[] CraterCreatureMorphPrerequisites;
}