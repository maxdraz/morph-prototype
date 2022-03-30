using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sense Weakness Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Sense Weakness")]
public class SenseWeaknessPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] SenseWeaknessStatPrerequisites;
    public MorphTypePrerequisite[] SenseWeaknessTypePrerequisites;
    public Morph[] SenseWeaknessMorphPrerequisites;

    public StatPrerequisite[] KillerConsumptionStatPrerequisites;
    public MorphTypePrerequisite[] KillerConsumptionTypePrerequisites;
    public Morph[] KillerConsumptionMorphPrerequisites;
}