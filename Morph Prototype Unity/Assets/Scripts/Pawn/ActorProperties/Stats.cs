using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stats : MonoBehaviour
{
    [SerializeField] private bool displayDebug;
    [SerializeField] private bool randomStats;
    private Rect debugWindowRect;
    private float rowOffset; 
    private float colOffset;
    private Vector2 origin;
    private Vector2 labelDimensions;
    private int row;
    private int col;

    private GUIStyle headerStyle;
    
    //core - Vector2 to define min and max base values
    //private Vector2 baseHealthPointsRange = new Vector2(500, 800);
    [SerializeField] private int baseMaxHealth;
    //private Vector2 baseEnergyPointsRange = new Vector2(100, 200);
    [SerializeField] private int baseMaxEnergy;
    //private Vector2 baseStaminaPointsRange = new Vector2(300, 500);
    [SerializeField] private int baseMaxStamina;
    //private Vector2 baseArmourPointsRange = new Vector2(100, 250);
    [SerializeField] private int baseMaxArmour;
    //base stats refer to the creatures base stats which were generated when the creature was spawned
    //added stats refer to all values being added to the base stats whether those values are permanent or temporary.
    //Morphs which add to stats when attached can add their bonus values to the added stats 
    //Total stats refer to the active value which is used when taking actions within the game (total = base+added)

    //offensive stats
    [SerializeField] private int baseMeleeDamage;
    private int addedMeleeDamage;
    public int totalMeleeDamage;
    [SerializeField] private int baseRangedDamage;
    private int addedRangedDamage;
    public int totalRangedDamage;
    [SerializeField] private int baseChemicalDamage;
    private int addedChemicalDamage;
    public int totalChemicalDamage;
    [SerializeField] private int baseElementalDamage;
    private int addedElementalDamage;
    public int totalElementalDamage;
    private int accuracy;
    //defensive stats
    [SerializeField] private int baseFortitude;
    private int addedFortitude;
    public int totalFortitude;
    [SerializeField] private int baseToughness;
    private int addedToughness;
    public int totalToughness;
    //misc stats 
    [SerializeField] private int baseIntimidation;
    private int addedIntimidation;
    public int totalIntimidation;
    [SerializeField] private int baseAgility;
    private int addedAgility;
    public int totalAgility;
    [SerializeField] private int baseStealth;
    private int addedStealth;
    public int totalStealth;
    [SerializeField] private int basePerception;
    private int addedPerception;
    public int totalPerception;
    [SerializeField] private int baseIntelligence;
    private int addedIntelligence;
    public int totalIntelligence;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseAttackSpeed;
    //resistance stats
    [SerializeField] private float baseFireResistance;
    private float addedFireResistance;
    public float totalFireResistance;
    [SerializeField] private float baseIceResistance;
    private float addedIceResistance;
    public float totalIceResistance;
    [SerializeField] private float baseElectricResistance;
    private float addedElectricResistance;
    public float totalElectricResistance;
    [SerializeField] private float basePoisonResistance;
    private float addedPoisonResistance;
    public float totalPoisonResistance;
    [SerializeField] private float baseAcidResistance;
    private float addedAcidResistance;
    public float totalAcidResistance;
    private float addedPhysicalResistance;
    public float totalPhysicalResistance;

    [SerializeField] private float meleeDamageModifier;
    [SerializeField] private float rangedDamageModifier;
    [SerializeField] private float chemicalDamageModifier;
    [SerializeField] private float elementalDamageModifier;

    [SerializeField] private float toughnessModifier;

    [SerializeField] private float maxEnergyModifier;
    [SerializeField] private float cooldownModifier;

    [SerializeField] private float attackSpeedModifier;
    [SerializeField] private float moveSpeedModifier;

    [SerializeField] private float maxStaminaModifier;

    StatModifiers statModifiers;

    //Health health;
    Stamina stamina;
    Energy energy;
    //Armor armor;
    Movement movement;

    //public interface
    public float MaxHealth => baseMaxHealth;
    public float MaxStamina => baseMaxStamina;
    public float MaxEnergy => baseMaxEnergy;
    public float MaxArmour => baseMaxArmour;
    public float ElementalDamageModifier => elementalDamageModifier;
    public float ChemicalDamageModifier => chemicalDamageModifier;
    public float MeleeDamageModifier => meleeDamageModifier;
    public float RangedDamageModifier => rangedDamageModifier;
    public float PoisonResistance => basePoisonResistance;
    public float ToughnessModifier => toughnessModifier;
    public float BaseMoveSpeed => baseMoveSpeed;
    public float BaseAttackSpeed => baseAttackSpeed;


    private void Reset()
    {
        debugWindowRect = new Rect(34, 18, 165, 374);
        rowOffset = 15;
        colOffset = 100;
        origin = new Vector2(10, 20);
        labelDimensions = new Vector2(100, 100);
    }

    private void Awake()
    {
        var statsModifierObj = GameObject.Find("StatsModifierManager");
        if(statsModifierObj)
            statModifiers = statsModifierObj.GetComponent<StatModifiers>();

        //health = GetComponent<Health>();
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();
        //armor = GetComponent<Armor>();
        movement = GetComponentInParent<Movement>();
        headerStyle = new GUIStyle();
        headerStyle.fontStyle = FontStyle.Bold;

        if (randomStats) 
        {
            RandomiseStats();
        }

        Invoke("SetStatTotals", .5f);
    }

    private void Update()
    {
    }

    private void OnGUI()
    {
        if(!displayDebug) return;
        
        debugWindowRect = GUI.Window(0, debugWindowRect, DrawStatsWindow, gameObject.name + " stats");
        
   }
 

    

    private void DrawStatsWindow(int windowID)
    {
        
        AddLabel("stat", "value",true);
        
        AddLabel("hp", baseMaxHealth.ToString());
        AddLabel("ep", baseMaxEnergy.ToString());
        AddLabel("stamina", baseMaxStamina.ToString());
        AddLabel();
        AddLabel("melee dmg", baseMeleeDamage.ToString());
        AddLabel("ranged dmg", baseRangedDamage.ToString());
        AddLabel("accuracy", accuracy.ToString());
        AddLabel();
        AddLabel("fortitude", baseFortitude.ToString());
        AddLabel("toughness", baseToughness.ToString());
        AddLabel();
        AddLabel("fire resist", baseFireResistance.ToString());
        AddLabel("ice resist", baseIceResistance.ToString());
        AddLabel("lightning resist", baseElectricResistance.ToString());
        AddLabel("poison resist", basePoisonResistance.ToString());
        AddLabel("acid resist", baseAcidResistance.ToString());
        AddLabel();
        AddLabel("intimidation", baseIntimidation.ToString());
        AddLabel("agility", baseAgility.ToString());
        AddLabel("stealth", baseStealth.ToString());
        AddLabel("perception", basePerception.ToString());
        AddLabel("intelligence", baseIntelligence.ToString());
        

        GUI.DragWindow();

        row = 0;
    }

    [Command("player-randomise-stats")]
    void RandomiseStats() 
    {
        baseMeleeDamage = Random.Range(10, 90);
        baseRangedDamage = Random.Range(10, 90);
        baseChemicalDamage = Random.Range(10, 90);
        baseElementalDamage = Random.Range(10, 90);
        baseIntelligence = Random.Range(10, 90);
        baseAgility = Random.Range(10, 90);
        baseToughness = Random.Range(10, 90);
        baseFortitude = Random.Range(10, 90);
        basePerception = Random.Range(10, 90);
        baseIntimidation = Random.Range(10, 90);
        baseStealth = Random.Range(10, 90);



        FindAllModifiers();
    }

    
    [Command("player-set-stats")]
    void SetStats(int mdmg, int rdmg, int cdmg, int edmg, int intell, int agil, int toughn)
    {
        baseMeleeDamage = mdmg;
        baseRangedDamage = rdmg;
        baseChemicalDamage = cdmg;
        baseElementalDamage = edmg;
        baseIntelligence = intell;
        baseAgility = agil;
        baseToughness = toughn;

        FindAllModifiers();
    }

    void AddLabel(string varName = "", string varValue = "", bool header = false)
    {
        var style = header ? headerStyle : GUI.skin.GetStyle("Label");
        
        GUI.Label(new Rect(origin.x + (colOffset * col), origin.y + (rowOffset * row), labelDimensions.x, labelDimensions.y), varName, style);
        col++;
        GUI.Label(new Rect(origin.x + (colOffset * col), origin.y + (rowOffset * row), labelDimensions.x, labelDimensions.y), varValue, style);
        row++;
        col = 0;
    }

    [Command("player-display-stats")]
    private void DisplayStatsWindow(bool bool_ShouldDisplay)
    {
        displayDebug = bool_ShouldDisplay;
    }

    public void FlatStatChange(string statName, int buffAmount)
    {
        //  Debug.Log("Buffing " + statName + " from " + statToBuff + " by " + buffAmount + " for " + duration + " seconds");
        if (statName == "meleeDamage")
        {

            totalMeleeDamage += buffAmount;
            addedMeleeDamage += buffAmount;

            FindModifier(statName, totalMeleeDamage);
        }
        if (statName == "rangedDamage")
        {
            totalRangedDamage += buffAmount;
            addedRangedDamage += buffAmount;

            FindModifier(statName, totalRangedDamage);
        }
        if (statName == "chemicalDamage")
        {

            totalChemicalDamage += buffAmount;
            addedChemicalDamage += buffAmount;

            FindModifier(statName, totalChemicalDamage);
        }
        if (statName == "elementalDamage")
        {

            totalElementalDamage += buffAmount;
            addedElementalDamage += buffAmount;

            FindModifier(statName, totalElementalDamage);
        }
        if (statName == "intelligence")
        {

            totalIntelligence += buffAmount;
            addedIntelligence += buffAmount;

            FindModifier(statName, totalIntelligence);
        }
        if (statName == "agility")
        {

            totalAgility += buffAmount;
            addedAgility += buffAmount;

            FindModifier(statName, totalAgility);
        }
        if (statName == "toughness")
        {

            totalToughness += buffAmount;
            addedToughness += buffAmount;

            FindModifier(statName, totalToughness);
        }
        if (statName == "fortitude")
        {

            totalFortitude += buffAmount;
            addedFortitude += buffAmount;

            FindModifier(statName, totalFortitude);
        }

        if (statName == "stealth")
        {

            totalStealth += buffAmount;
            addedStealth += buffAmount;

        }

        if (statName == "intimidation")
        {

            totalIntimidation += buffAmount;
            addedIntimidation += buffAmount;

        }

        if (statName == "perception")
        {

            totalPerception += buffAmount;
            addedPerception += buffAmount;

        }
    }

    

    public void PercentStatChange(string statName, int buffAmount)
    {
        float changeInValue;

        if (statName == "meleeDamage")
        {

            float valueToChange = baseMeleeDamage;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalMeleeDamage = (int)valueToChange;
            addedMeleeDamage += (int)changeInValue; ;

            FindModifier(statName, totalMeleeDamage);
        }
        if (statName == "rangedDamage")
        {
            float valueToChange = baseRangedDamage;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalRangedDamage = (int)valueToChange;
            addedRangedDamage += (int)changeInValue;

            FindModifier(statName, totalRangedDamage);
        }
        if (statName == "chemicalDamage")
        {
            float valueToChange = baseChemicalDamage;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalChemicalDamage = (int)valueToChange;
            addedChemicalDamage += (int)changeInValue;

            FindModifier(statName, totalChemicalDamage);
        }
        if (statName == "elementalDamage")
        {
            float valueToChange = baseElementalDamage;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalElementalDamage = (int)valueToChange;
            addedElementalDamage += (int)changeInValue;

            FindModifier(statName, totalElementalDamage);
        }
        if (statName == "intelligence")
        {
            float valueToChange = baseIntelligence;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalIntelligence = (int)valueToChange;
            addedIntelligence += (int)changeInValue;

            FindModifier(statName, totalIntelligence);
        }
        if (statName == "agility")
        {
            float valueToChange = baseAgility;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalAgility = (int)valueToChange;
            addedAgility += (int)changeInValue;

            FindModifier(statName, totalAgility);
        }
        if (statName == "toughness")
        {
            float valueToChange = baseToughness;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalToughness = (int)valueToChange;
            addedToughness += (int)changeInValue;

            FindModifier(statName, totalToughness);
        }
        if (statName == "fortitude")
        {
            float valueToChange = baseFortitude;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalFortitude = (int)valueToChange;
            addedFortitude += (int)changeInValue;

            FindModifier(statName, totalFortitude);
        }
    }

    public void PercentResistStatChange(string damageType, float buffAmount)
    {
        float changeInValue;

        //  Debug.Log("Buffing " + statName + " from " + statToBuff + " by " + buffAmount + " for " + duration + " seconds");
        if (damageType == "fire")
        {

            float valueToChange = baseFireResistance;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalFireResistance = (int)valueToChange;
            addedFireResistance += (int)changeInValue; ;

        }
        if (damageType == "ice")
        {
            float valueToChange = baseIceResistance;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalIceResistance = (int)valueToChange;
            addedIceResistance += (int)changeInValue; ;

        }
        if (damageType == "electric")
        {

            float valueToChange = baseFireResistance;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalElectricResistance = (int)valueToChange;
            addedElectricResistance += (int)changeInValue; ;

        }
        if (damageType == "poison")
        {

            float valueToChange = baseFireResistance;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalPoisonResistance = (int)valueToChange;
            addedPoisonResistance += (int)changeInValue; ;

        }
        if (damageType == "acid")
        {

            float valueToChange = baseFireResistance;
            changeInValue = valueToChange * buffAmount;
            valueToChange += changeInValue;
            totalAcidResistance = (int)valueToChange;
            addedAcidResistance += (int)changeInValue; ;

        }
    }

    public void FlatResistStatChange(string damageType, float buffAmount)
    {
        //  Debug.Log("Buffing " + statName + " from " + statToBuff + " by " + buffAmount + " for " + duration + " seconds");
        if (damageType == "fire")
        {

            totalFireResistance += buffAmount;
            addedFireResistance += buffAmount;

        }
        if (damageType == "ice")
        {
            totalIceResistance += buffAmount;
            addedIceResistance += buffAmount;

        }
        if (damageType == "electric")
        {

            totalElectricResistance += buffAmount;
            addedElectricResistance += buffAmount;

        }
        if (damageType == "poison")
        {

            totalPoisonResistance += buffAmount;
            addedPoisonResistance += buffAmount;

        }
        if (damageType == "acid")
        {

            totalAcidResistance += buffAmount;
            addedAcidResistance += buffAmount;

        }
    }

    void FindAllModifiers() 
    {
        FindModifier("meleeDamage", totalMeleeDamage);
        FindModifier("rangedDamage", totalRangedDamage);
        FindModifier("chemicalDamage", totalChemicalDamage);
        FindModifier("elementalDamage", totalElementalDamage);
        FindModifier("intelligence", totalIntelligence);
        FindModifier("agility", totalAgility);
        FindModifier("toughness", totalToughness);
        FindModifier("fortitude", totalFortitude);

    }

    void FindModifier(string myStat, int myStatValue) 
    {
        //Debug.Log("Called Find " + myStat + " Modifier");
        if (!statModifiers) return;
        
        if (myStat == "meleeDamage") 
        {

            for (int i = 0; i < statModifiers.mDMGModifiers.Count; ++i)
            {
                if (statModifiers.mDMGModifiers[i].x <= myStatValue)
                {

                    meleeDamageModifier = statModifiers.mDMGModifiers[i].y;
                    
                }
            }
        }
        if (myStat == "rangedDamage")
        {

            for (int i = 0; i < statModifiers.rDMGModifiers.Count; ++i)
            {
                if (statModifiers.rDMGModifiers[i].x <= myStatValue)
                {

                    rangedDamageModifier = statModifiers.rDMGModifiers[i].y;
                    
                }
            }
        }
        if (myStat == "chemicalDamage")
        {

            for (int i = 0; i < statModifiers.cDMGModifiers.Count; ++i)
            {
                if (statModifiers.cDMGModifiers[i].x <= myStatValue)
                {

                    chemicalDamageModifier = statModifiers.cDMGModifiers[i].y;
                    
                }
            }
        }
        if (myStat == "elementalDamage")
        {

            for (int i = 0; i < statModifiers.eDMGModifiers.Count; ++i)
            {
                if (statModifiers.eDMGModifiers[i].x <= myStatValue)
                {

                    elementalDamageModifier = statModifiers.eDMGModifiers[i].y;
                    
                }
            }
        }
        if (myStat == "toughness")
        {

            for (int i = 0; i < statModifiers.toughnessDamageReductionModifiers.Count; ++i)
            {
                if (statModifiers.toughnessDamageReductionModifiers[i].x <= myStatValue)
                {

                    toughnessModifier = statModifiers.toughnessDamageReductionModifiers[i].y;
                    
                }
            }
        }

        if (myStat == "fortitude")
        {

            for (int i = 0; i < statModifiers.fortitudeMaxStaminaModifiers.Count; ++i)
            {
                if (statModifiers.fortitudeMaxStaminaModifiers[i].x <= myStatValue && myStatValue - statModifiers.fortitudeMaxStaminaModifiers[i].x <= 4 && myStatValue - statModifiers.fortitudeMaxStaminaModifiers[i].x >= 0)
                {

                    maxStaminaModifier = statModifiers.fortitudeMaxStaminaModifiers[i].y;
                    stamina.maxStaminaBonus += maxStaminaModifier;
                }
            }
        }

        if (myStat == "intelligence")
        {

            for (int i = 0; i < statModifiers.intelligenceCooldownReductionModifiers.Count; ++i)
            {
                if (statModifiers.intelligenceCooldownReductionModifiers[i].x <= myStatValue)
                {

                    cooldownModifier = statModifiers.intelligenceCooldownReductionModifiers[i].y;
                    
                }
            }
            for (int i = 0; i < statModifiers.intelligenceMaxEnergyModifiers.Count; ++i)
            {
                if (statModifiers.intelligenceMaxEnergyModifiers[i].x <= myStatValue && myStatValue - statModifiers.intelligenceMaxEnergyModifiers[i].x <= 4 && myStatValue - statModifiers.intelligenceMaxEnergyModifiers[i].x >= 0)
                {

                    maxEnergyModifier = statModifiers.intelligenceMaxEnergyModifiers[i].y;
                    energy.bonusMaxEnergy += maxEnergyModifier;

                }
            }
        }

        if (myStat == "agility")
        {

            for (int i = 0; i < statModifiers.agilityAttackSpeedModifiers.Count; ++i)
            {
                if (statModifiers.agilityAttackSpeedModifiers[i].x <= myStatValue)
                {

                    attackSpeedModifier = statModifiers.agilityAttackSpeedModifiers[i].y;
                    
                }
            }
            for (int i = 0; i < statModifiers.agilityMoveSpeedModifiers.Count; ++i)
            {
                if (statModifiers.agilityMoveSpeedModifiers[i].x <= myStatValue && myStatValue - statModifiers.agilityMoveSpeedModifiers[i].x <= 4 && myStatValue - statModifiers.agilityMoveSpeedModifiers[i].x >= 0)
                {
                    moveSpeedModifier = statModifiers.agilityMoveSpeedModifiers[i].y;
                    movement.bonusPercentMoveSpeed += moveSpeedModifier;
                    ///Debug.Log("adding " + moveSpeedModifier + " speed from findallmodifiers_stats");

                }
            }
        }
    }

    

    private void SetStatTotals() 
    {
        totalMeleeDamage = baseMeleeDamage + addedMeleeDamage;
        totalRangedDamage = baseRangedDamage + addedRangedDamage;
        totalChemicalDamage = baseChemicalDamage + addedChemicalDamage;
        totalElementalDamage = baseElementalDamage + addedElementalDamage;
        totalAgility = baseAgility + addedAgility;
        totalFortitude = baseFortitude + addedFortitude;
        totalIntelligence = baseIntelligence + addedIntelligence;

        totalIntimidation = baseIntimidation + addedIntimidation;
        GetComponent<Intimidation>().SetMaxIntimidation(totalIntimidation);

        totalPerception = basePerception + addedPerception;
        GetComponent<Perception>().SetMaxPerception(totalPerception);

        totalStealth = baseStealth + addedStealth;
        GetComponent<Stealth>().SetMaxStealth(totalStealth);

        totalToughness = baseToughness + addedToughness;

        FindAllModifiers();

        totalFireResistance = baseFireResistance + addedFireResistance;

        totalIceResistance = baseIceResistance + addedIceResistance;

        totalElectricResistance = baseElectricResistance + addedElectricResistance;

        totalPoisonResistance = basePoisonResistance + addedPoisonResistance;

        totalAcidResistance = baseAcidResistance + addedAcidResistance;

        totalPhysicalResistance = toughnessModifier + addedPhysicalResistance;

    }
}
