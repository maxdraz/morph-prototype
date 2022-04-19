using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow Blow Prerequisite Data", menuName = "Attack Morph Prerequisite Data/Shadow Blow")]
public class ShadowBlowPrerequisiteData : ScriptableObject
{
    //Shadow Blow prerequisits
    public StatPrerequisite[] ShadowBlowStatPrerequisites;
    public MorphTypePrerequisite[] ShadowBlowTypePrerequisites;
    public Morph[] ShadowBlowMorphPrerequisites;
}