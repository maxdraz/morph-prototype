using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Morph
{
    float rarity;
    float uncommonRarity = 50f;
    float rareRarity = 70f;
    float legendaryRarity = 90f;

    int amountToBoostUncommon;

    int amountToBoostRare;

    int amountToBoostLegendary;

    public string rarityLevel;


    void Awake() 
    {
       
        //We need to reference the morph type and add stats based on the morph type. It will have stats associated with it which can be boosted 


    }

    protected virtual void AddStats() 
    {
        //This will be used to add stats when the morph is attached. Weapon, special, or passive morphs can all modify stats

        //morph.commonStat += amountToBoostCommon;

        if (rarityLevel == "Uncommon") 
        {
            //morph.uncommonStat += amountToBoostUncommon;
        }
        if (rarityLevel == "Rare")
        {
            //morph.rareStat += amountToBoostRare;
        }
        if (rarityLevel == "Legendary")
        {
            //morph.legendaryStat += amountToBoostLegendary;
        }
    }

    protected virtual void RemoveStats()
    {
        //This will be used to add stats when the morph is attached. Weapon, special, or passive morphs can all modify stats

        //morph.commonStat += morph.amountToBoostCommon; This is a flat value unlike the other stat boosts which are variable

        if (rarityLevel == "Uncommon")
        {
            //morph.uncommonStat -= amountToBoostUncommon;
        }
        if (rarityLevel == "Rare")
        {
            //morph.rareStat -= amountToBoostRare;
        }
        if (rarityLevel == "Legendary")
        {
            //morph.legendaryStat -= amountToBoostLegendary;
        }
    }


    void GenerateStatBoosts() 
    {
        //uncommonBoost = Random.Range(minStatBoost,maxStatBoost);
        //amountToBoostUncommon += uncommonBoost;

        //rareBoost = Random.Range(minStatBoost,maxStatBoost);
        //amountToBoostRare += rareBoost;

        //legendaryBoost = Random.Range(minStatBoost,maxStatBoost);
        //amountToBoostLegendary += legendaryBoost;


    }

    protected virtual void RarityRoll() 
    {
        //When the morph is generated this will roll to determine the rarity, and generate stats to boost when it is attached
        rarity = Random.Range(0, 100);

        rarityLevel = "Common";

        if (rarity > uncommonRarity) 
        {
            rarityLevel = "Uncommon";

        }

        if (rarity > rareRarity)
        {
            rarityLevel = "Rare";

        }

        if (rarity > legendaryRarity)
        {
            rarityLevel = "Legendary";

        }
    }
}
