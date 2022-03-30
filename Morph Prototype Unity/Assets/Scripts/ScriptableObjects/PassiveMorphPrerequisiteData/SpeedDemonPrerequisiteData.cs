using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Speed Demon Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Speed Demon")]
public class SpeedDemonPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] SpeedDemonStatPrerequisites;
    public MorphTypePrerequisite[] SpeedDemonTypePrerequisites;
    public Morph[] SpeedDemonMorphPrerequisites;

    public StatPrerequisite[] CruelCapacityStatPrerequisites;
    public MorphTypePrerequisite[] CruelCapacityTypePrerequisites;
    public Morph[] CruelCapacityMorphPrerequisites;


}
