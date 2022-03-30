using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrifying Howl Prerequisite Data", menuName = "Active Morph Prerequisite Data/Terrifying Howl")]
public class TerrifyingHowlPrerequisiteData : ScriptableObject
{
    //Terrifying Howl Barrage prerequisites
    public StatPrerequisite[] TerrifyingHowlStatPrerequisites;
    public MorphTypePrerequisite[] TerrifyingHowlTypePrerequisites;
    public Morph[] TerrifyingHowlMorphPrerequisites;
}