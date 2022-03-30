using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Gaseous Discharge Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Gaseous Discharge")]
public class GaseousDischargePrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] GaseousDischargeStatPrerequisites;
    public MorphTypePrerequisite[] GaseousDischargeTypePrerequisites;
    public Morph[] GaseousDischargeMorphPrerequisites;

    public StatPrerequisite[] ToxicOverflowStatPrerequisites;
    public MorphTypePrerequisite[] ToxicOverflowTypePrerequisites;
    public Morph[] ToxicOverflowMorphPrerequisites;
}