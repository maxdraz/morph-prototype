using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ferocity Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Ferocity")]
public class FerocityPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] FerocityStatPrerequisites;
    public MorphTypePrerequisite[] FerocityTypePrerequisites;
    public Morph[] FerocityMorphPrerequisites;

    public StatPrerequisite[] SpiritSiphonStatPrerequisites;
    public MorphTypePrerequisite[] SpiritSiphonTypePrerequisites;
    public Morph[] SpiritSiphonMorphPrerequisites; 
}
