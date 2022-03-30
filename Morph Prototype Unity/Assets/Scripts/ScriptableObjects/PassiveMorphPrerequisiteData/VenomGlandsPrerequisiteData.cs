using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Venom Glands Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Venom Glands")]
public class VenomGlandsPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] VenomGlandsStatPrerequisites;
    public MorphTypePrerequisite[] VenomGlandsTypePrerequisites;
    public Morph[] VenomGlandsMorphPrerequisites;

    public StatPrerequisite[] AtrophyVenomStatPrerequisites;
    public MorphTypePrerequisite[] AtrophyVenomTypePrerequisites;
    public Morph[] AtrophyVenomMorphPrerequisites;

    public StatPrerequisite[] AcridVenomStatPrerequisites;
    public MorphTypePrerequisite[] AcridVenomTypePrerequisites;
    public Morph[] AcridVenomMorphPrerequisites;

    public StatPrerequisite[] HazeVenomStatPrerequisites;
    public MorphTypePrerequisite[] HazeVenomStatTypePrerequisites;
    public Morph[] HazeVenomStatMorphPrerequisites;

    public StatPrerequisite[] AnaestheticVenomStatPrerequisites;
    public MorphTypePrerequisite[] AnaestheticVenomTypePrerequisites;
    public Morph[] AnaestheticVenomMorphPrerequisites;
}