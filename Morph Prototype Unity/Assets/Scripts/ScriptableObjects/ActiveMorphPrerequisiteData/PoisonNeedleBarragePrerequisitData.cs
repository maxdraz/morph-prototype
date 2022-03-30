using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison Needle Barrage Prerequisite Data", menuName = "Morph Prerequisite Data/Poison Needle Barrage")]
public class PoisonNeedleBarragePrerequisiteData : ScriptableObject
{
    //Poison Needle Barrage prerequisites
    public StatPrerequisite[] PoisonNeedleBarrageStatPrerequisites;
    public MorphTypePrerequisite[] PoisonNeedleBarrageTypePrerequisites;
    public Morph[] PoisonNeedleBarrageMorphPrerequisites;


    //Neurotoxic Needle  prerequisites
    public StatPrerequisite[] NeurotoxicNeedleStatPrerequisites;
    public MorphTypePrerequisite[] NeurotoxicNeedleTypePrerequisites;
    public Morph[] NeurotoxicNeedleMorphPrerequisites;
}