using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Adrenaline Rush Prerequisite Data", menuName = "Active Morph Prerequisite Data/Adrenaline Rush")]
public class AdrenalineRushPrerequisiteData : ScriptableObject
{
    //Adrenaline Rush prerequisits
    public StatPrerequisite[] AdrenalineRushStatPrerequisites;
    public MorphTypePrerequisite[] AdrenalineRushTypePrerequisites;
    public Morph[] AdrenalineRushMorphPrerequisites;

    //Unnatural Vigor prerequisits
    public StatPrerequisite[] UnnaturalVigorStatPrerequisites;
    public MorphTypePrerequisite[] UnnaturalVigorTypePrerequisites;
    public Morph[] UnnaturalVigorMorphPrerequisites;
}
