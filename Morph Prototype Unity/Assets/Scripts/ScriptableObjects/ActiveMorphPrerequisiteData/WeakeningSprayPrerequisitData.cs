using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weakening Spray Prerequisite Data", menuName = "Active Morph Prerequisite Data/Weakening Spray")]
public class WeakeningSprayPrerequisiteData: ScriptableObject
{
    //Weakening Spray Barrage prerequisites
    public StatPrerequisite[] WeakeningSprayStatPrerequisites;
    public MorphTypePrerequisite[] WeakeningSprayTypePrerequisites;
    public Morph[] WeakeningSprayMorphPrerequisites;


    //Viscous Blast prerequisites
    public StatPrerequisite[] ViscousBlastStatPrerequisites;
    public MorphTypePrerequisite[] ViscousBlastTypePrerequisites;
    public Morph[] ViscousBlastMorphPrerequisites;
}