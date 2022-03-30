using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Supersonic Strikes Prerequisite Data", menuName = "Attack Morph Prerequisite Data/Supersonic Strikes")]
public class SupersonicStrikesPrerequisiteData: ScriptableObject
{
    //Supersonic Strikes prerequisits
    public StatPrerequisite[] SupersonicStrikesStatPrerequisites;
    public MorphTypePrerequisite[] SupersonicStrikesTypePrerequisites;
    public Morph[] SupersonicStrikesMorphPrerequisites;
}