using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash Dash Prerequisite Data", menuName = "Attack Morph Prerequisite Data/Slash Dash")]
public class SlashDashPrerequisiteData : ScriptableObject
{
    //Slash Dash prerequisits
    public StatPrerequisite[] SlashDashStatPrerequisites;
    public MorphTypePrerequisite[] SlashDashTypePrerequisites;
    public Morph[] SlashDashMorphPrerequisites;
}