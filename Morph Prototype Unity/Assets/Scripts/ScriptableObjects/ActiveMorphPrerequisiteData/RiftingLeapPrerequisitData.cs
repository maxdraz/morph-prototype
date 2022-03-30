using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rifting Leap Prerequisite Data", menuName = "Morph Prerequisite Data/Rifting Leap")]
public class RiftingLeapPrerequisiteData : ScriptableObject
{
    //Rifting Leap Barrage prerequisites
    public StatPrerequisite[] RiftingLeapStatPrerequisites;
    public MorphTypePrerequisite[] RiftingLeapTypePrerequisites;
    public Morph[] RiftingLeapMorphPrerequisites;
}