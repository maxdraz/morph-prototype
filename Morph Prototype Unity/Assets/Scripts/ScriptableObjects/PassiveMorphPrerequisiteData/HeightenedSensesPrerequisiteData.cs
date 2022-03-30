using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Heightened Senses Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Heightened Senses")]
public class HeightenedSensesPrerequisiteData : ScriptableObject
{
    //Heightened Senses Barrage prerequisites
    public StatPrerequisite[] HeightenedSensesStatPrerequisites;
    public MorphTypePrerequisite[] HeightenedSensesTypePrerequisites;
    public Morph[] HeightenedSensesMorphPrerequisites;

    //Ever Ready Barrage prerequisites
    public StatPrerequisite[] EverReadyStatPrerequisites;
    public MorphTypePrerequisite[] EverReadyTypePrerequisites;
    public Morph[] EverReadyMorphPrerequisites;

    //Aimed Strikes Barrage prerequisites
    public StatPrerequisite[] AimedStrikesStatPrerequisites;
    public MorphTypePrerequisite[] AimedStrikesTypePrerequisites;
    public Morph[] AimedStrikesMorphPrerequisites;
}
