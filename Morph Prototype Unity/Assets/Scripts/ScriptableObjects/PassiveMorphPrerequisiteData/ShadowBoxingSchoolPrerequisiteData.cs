using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow Boxing School Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Shadow Boxing School")]
public class ShadowBoxingSchoolPrerequisiteData : ScriptableObject

{
    public StatPrerequisite[] ShadowBoxingSchoolStatPrerequisites;
    public MorphTypePrerequisite[] ShadowBoxingSchoolTypePrerequisites;
    public Morph[] ShadowBoxingSchoolMorphPrerequisites;

    public StatPrerequisite[] NinjaTrainingStatPrerequisites;
    public MorphTypePrerequisite[] NinjaTrainingTypePrerequisites;
    public Morph[] NinjaTrainingMorphPrerequisites;
}
