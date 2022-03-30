using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pox Shot Prerequisite Data", menuName = "Active Morph Prerequisite Data/Pox Shot")]
public class PoxShotPrerequisiteData : ScriptableObject
{
    //Poison Needle Barrage prerequisites
    public StatPrerequisite[] PoxShotBarrageStatPrerequisites;
    public MorphTypePrerequisite[] PoxShotBarrageTypePrerequisites;
    public Morph[] PoxShotBarrageMorphPrerequisites;


    //Chemical Cannon prerequisites
    public StatPrerequisite[] ChemicalCannonStatPrerequisites;
    public MorphTypePrerequisite[] ChemicalCannonTypePrerequisites;
    public Morph[] ChemicalCannonMorphPrerequisites;
}