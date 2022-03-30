using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Statue Stealth Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Statue Stealth")]
public class StatueStealthPrerequisiteData: ScriptableObject
{
    //Statue Stealth Barrage prerequisites
    public StatPrerequisite[] StatueStealthStatPrerequisites;
    public MorphTypePrerequisite[] StatueStealthTypePrerequisites;
    public Morph[] StatueStealthMorphPrerequisites;

    //Hidden Threat Barrage prerequisites
    public StatPrerequisite[] HiddenThreatStatPrerequisites;
    public MorphTypePrerequisite[] HiddenThreatTypePrerequisites;
    public Morph[] HiddenThreatMorphPrerequisites;
}
