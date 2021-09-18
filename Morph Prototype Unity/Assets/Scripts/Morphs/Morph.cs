using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Morph
{
    protected virtual void AddStats() 
    {
    //This will be used to add stats when the morph is attached. Weapon, special, or passive morphs can all modify stats, but only spawn with bonus stats if they are rare
    }

    protected virtual void RarityRoll() 
    {
    //When the morph is generated this will roll to determine the rarity, and generate stats to boost when it is attached
    }
}