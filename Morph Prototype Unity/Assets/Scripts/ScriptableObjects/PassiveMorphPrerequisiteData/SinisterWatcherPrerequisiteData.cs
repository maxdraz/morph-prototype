using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sinister Watcher Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Sinister Watcher")]
public class SinisterWatcherPrerequisiteData : ScriptableObject

{
    public StatPrerequisite[] StatueStealthStatPrerequisites;
    public MorphTypePrerequisite[] StatueStealthTypePrerequisites;
    public Morph[] StatueStealthMorphPrerequisites;

    public StatPrerequisite[] UnknownSourceStatPrerequisites;
    public MorphTypePrerequisite[] UnknownSourceTypePrerequisites;
    public Morph[] UnknownSourceMorphPrerequisites;
}
