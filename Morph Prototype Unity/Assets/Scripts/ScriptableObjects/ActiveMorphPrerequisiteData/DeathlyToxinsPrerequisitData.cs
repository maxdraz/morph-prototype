using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deathly Toxins Prerequisite Data", menuName = "Morph Prerequisite Data/Deathly Toxins")]
public class DeathlyToxinsPrerequisiteData : ScriptableObject
{
    //Deathly Toxins prerequisites
    public StatPrerequisite[] DeathlyToxinsStatPrerequisites;
    public MorphTypePrerequisite[] DeathlyToxinsTypePrerequisites;
    public Morph[] DeathlyToxinsMorphPrerequisites;


    //Chemical Cocktail prerequisites
    public StatPrerequisite[] ChemicalCocktailStatPrerequisites;
    public MorphTypePrerequisite[] ChemicalCocktailTypePrerequisites;
    public Morph[] ChemicalCocktailMorphPrerequisites;

}

