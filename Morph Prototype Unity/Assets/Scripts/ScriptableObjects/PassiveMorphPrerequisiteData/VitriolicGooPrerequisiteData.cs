using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vitriolic Goo Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Vitriolic Goo")]
public class VitriolicGooPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] VitriolicGooStatPrerequisites;
    public MorphTypePrerequisite[] VitriolicGooTypePrerequisites;
    public Morph[] VitriolicGooMorphPrerequisites;

    public StatPrerequisite[] MolecularAcidStatPrerequisites;
    public MorphTypePrerequisite[] MolecularAcidTypePrerequisites;
    public Morph[] MolecularAcidMorphPrerequisites;
}