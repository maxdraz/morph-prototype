using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Immobalising Crash Prerequisite Data", menuName = "Active Morph Prerequisite Data/Immobalising Crash")]
public class ImmobalisingCrashPrerequisiteData: ScriptableObject
{

    //Immobalising Crash Barrage prerequisites
    public StatPrerequisite[] ImmobalisingCrashStatPrerequisites;
    public MorphTypePrerequisite[] ImmobalisingCrashTypePrerequisites;
    public Morph[] ImmobalisingCrashMorphPrerequisites;
}



