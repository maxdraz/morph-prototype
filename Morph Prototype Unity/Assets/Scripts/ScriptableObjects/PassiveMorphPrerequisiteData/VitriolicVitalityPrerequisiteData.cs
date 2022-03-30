using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Vitriolic Vitality Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Vitriolic Vitality")]
public class VitriolicVitalityPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] VitriolicVitalityStatPrerequisites;
    public MorphTypePrerequisite[] VitriolicVitalityTypePrerequisites;
    public Morph[] VitriolicVitalityMorphPrerequisites;

    public StatPrerequisite[] VenomousVigorStatPrerequisites;
    public MorphTypePrerequisite[] VenomousVigorTypePrerequisites;
    public Morph[] VenomousVigorMorphPrerequisites;

    public StatPrerequisite[] ToxicFocusStatPrerequisites;
    public MorphTypePrerequisite[] ToxicFocusTypePrerequisites;
    public Morph[] ToxicFocusMorphPrerequisites;
}
    