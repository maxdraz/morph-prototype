using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Acid Eruption Prerequisite Data", menuName = "Morph Prerequisite Data/Acid Eruption")]
    public class AcidEruptionPrerequisiteData: ScriptableObject
    {
    //Acid Eruption prerequisits
    public StatPrerequisite[] AcidEruptionStatPrerequisites;
    public MorphTypePrerequisite[] AcidEruptionTypePrerequisites;
    public Morph[] AcidEruptionMorphPrerequisites;

    //Acid Vortex prerequisits
    public StatPrerequisite[] AcidVortexStatPrerequisites;
    public MorphTypePrerequisite[] AcidVortexTypePrerequisites;
    public Morph[] AcidVortexMorphPrerequisites;
}

