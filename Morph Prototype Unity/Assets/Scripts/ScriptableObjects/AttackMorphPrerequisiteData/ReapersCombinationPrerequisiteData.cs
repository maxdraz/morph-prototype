using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reapers Combination Prerequisite Data", menuName = "Attack Morph Prerequisite Data/Reapers Combination")]
public class ReapersCombinationPrerequisiteData : ScriptableObject
{
    //Reapers Combination prerequisits
    public StatPrerequisite[] ReapersCombinationStatPrerequisites;
    public MorphTypePrerequisite[] ReapersCombinationTypePrerequisites;
    public Morph[] ReapersCombinationMorphPrerequisites;
}