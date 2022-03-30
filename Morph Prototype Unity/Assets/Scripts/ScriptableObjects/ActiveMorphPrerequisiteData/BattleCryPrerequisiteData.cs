using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Battle Cry Prerequisite Data", menuName = "Morph Prerequisite Data/Battle Cry")]
    public class BattleCryPrerequisiteData : ScriptableObject
    {
    //Battle Cry prerequisites
    public StatPrerequisite[] BattleCryStatPrerequisites;
    public MorphTypePrerequisite[] BattleCryTypePrerequisites;
    public Morph[] BattleCryMorphPrerequisites;
}

