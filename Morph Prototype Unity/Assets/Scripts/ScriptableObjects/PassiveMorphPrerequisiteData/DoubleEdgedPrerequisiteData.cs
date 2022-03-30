using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Double Edged Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Double Edged")]
public class DoubleEdgedPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] DoubleEdgedStatPrerequisites;
    public MorphTypePrerequisite[] DoubleEdgedTypePrerequisites;
    public Morph[] DoubleEdgedMorphPrerequisites;

    public StatPrerequisite[] BloodGuzzlerStatPrerequisites;
    public MorphTypePrerequisite[] BloodGuzzlerTypePrerequisites;
    public Morph[] BloodGuzzlerMorphPrerequisites;
}