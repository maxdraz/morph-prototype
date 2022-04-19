using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Foul Fungus Prerequisite Data", menuName = "Active Morph Prerequisite Data/Foul Fungus")]
public class FoulFungusPrerequisiteData : ScriptableObject
{
    //Foul Fungus Barrage prerequisites
    public StatPrerequisite[] FoulFungusStatPrerequisites;
    public MorphTypePrerequisite[] FoulFungusTypePrerequisites;
    public Morph[] FoulFungusMorphPrerequisites;
}


