using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Intercept Prerequisite Data", menuName = "Active Morph Prerequisite Data/Intercept")]
public class InterceptCrashPrerequisiteData : ScriptableObject
{
    //Intercept prerequisits
    public StatPrerequisite[] InterceptStatPrerequisites;
    public MorphTypePrerequisite[] InterceptTypePrerequisites;
    public Morph[] InterceptMorphPrerequisites;
}


