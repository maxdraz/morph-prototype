using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Endless Aggression Prerequisite Data", menuName = "Passive Morph Prerequisite Data/Endless Aggression")]
public class EndlessAggressionPrerequisiteData : ScriptableObject
{
    public StatPrerequisite[] EndlessAggressionStatPrerequisites;
    public MorphTypePrerequisite[] EndlessAggressionTypePrerequisites;
    public Morph[] EndlessAggressionMorphPrerequisites;

    public StatPrerequisite[] ExplosiveAngerStatPrerequisites;
    public MorphTypePrerequisite[] ExplosiveAngerTypePrerequisites;
    public Morph[] ExplosiveAngerMorphPrerequisites;
}
