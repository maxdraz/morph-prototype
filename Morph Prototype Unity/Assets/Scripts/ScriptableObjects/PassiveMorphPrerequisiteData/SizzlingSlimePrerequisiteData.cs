using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sizzling Slime Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Sizzling Slime")]
public class SizzlingSlimePrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] SizzlingSlimeStatPrerequisites;
    public MorphTypePrerequisite[] SizzlingSlimeTypePrerequisites;
    public Morph[] SizzlingSlimeMorphPrerequisites;

    public StatPrerequisite[] BlindingVapourStatPrerequisites;
    public MorphTypePrerequisite[] BlindingVapourTypePrerequisites;
    public Morph[] BlindingVapourMorphPrerequisites;
}