using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Finishing Strike Prerequisite Data", menuName = "Attack Morph Prerequisite Data/Finishing Strike")]
public class FinishingStrikePrerequisiteData : ScriptableObject
{
    //Finishing Strike prerequisits
    public StatPrerequisite[] FinishingStrikeStatPrerequisites;
    public MorphTypePrerequisite[] FinishingStrikeTypePrerequisites;
    public Morph[] FinishingStrikeMorphPrerequisites;
}