using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colour Change Prerequisite Data", menuName = "Active Morph Prerequisite Data/Colour Change")]
public class ColourChangePrerequisiteData : ScriptableObject
{
    //Colour Change prerequisits
    public StatPrerequisite[] ColourChangeStatPrerequisites;
    public MorphTypePrerequisite[] ColourChangeTypePrerequisites;
    public Morph[] ColourChangeMorphPrerequisites;

    //Shimmering prerequisits
    public StatPrerequisite[] ShimmeringStatPrerequisites;
    public MorphTypePrerequisite[] ShimmeringTypePrerequisites;
    public Morph[] ShimmeringMorphPrerequisites;

    //Bioluminescent Flash prerequisits
    public StatPrerequisite[] BioluminescentFlashStatPrerequisites;
    public MorphTypePrerequisite[] BioluminescentFlashTypePrerequisites;
    public Morph[] BioluminescentFlashMorphPrerequisites;
}

