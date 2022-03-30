using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Nasty Fumes Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Nasty Fumes")]
public class NastyFumesPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] NastyFumesStatPrerequisites;
    public MorphTypePrerequisite[] NastyFumesTypePrerequisites;
    public Morph[] NastyFumesMorphPrerequisites;

    public StatPrerequisite[] NastyBurstStatPrerequisites;
    public MorphTypePrerequisite[] NastyBurstTypePrerequisites;
    public Morph[] NastyBurstMorphPrerequisites;
}
