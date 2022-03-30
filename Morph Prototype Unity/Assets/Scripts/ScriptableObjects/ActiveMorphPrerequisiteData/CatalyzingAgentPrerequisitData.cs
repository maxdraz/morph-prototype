using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Catalyzing Agent Prerequisite Data", menuName = "Morph Prerequisite Data/Catalyzing Agent")]
    public class CatalyzingAgentPrerequisiteData: ScriptableObject
    {
    //Catalyzing Agent Barrage prerequisites
    public StatPrerequisite[] CatalyzingAgentStatPrerequisites;
    public MorphTypePrerequisite[] CatalyzingAgentTypePrerequisites;
    public Morph[] CatalyzingAgentMorphPrerequisites;
}
