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
    
    //core
    private int healthPoints;
    private int healthRegen;
    private int energyPoints;
    private int energyRegen;
    private int staminaPoints;
    private int staminaRegen;
    //offensive
    private int meleeDamage;
    private int rangedDamage;
    private int chemicalDamage;
    private int elementalDamage;
    private int accuracy;
    //defensive
    private int fortitude;
    private int toughness;
    //misc
    private int intimidation;
    private int agility;
    private int stealth;
    private int perception;
    private int intelligence;
    //resistances
    private int fireResistance;
    private int iceResistance;
    private int lightningResistance;
    private int poisonResistance;
    private int acidResistance;

    [SerializeField] private float meleeDamageModifier;
    [SerializeField] private float rangedDamageModifier;
    [SerializeField] private float chemicalDamageModifier;
    [SerializeField] private float elementalDamageModifier;

    [SerializeField] private float toughnessModifier;

    [SerializeField] private float energyRegenModifier;
    [SerializeField] private float cooldownModifier;

    [SerializeField] private float attackSpeedModifier;
    [SerializeField] private float moveSpeedModifier;

    StatModifiers statModifiers;
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
        statModifiers = GameObject.Find("StatsModifierManager").GetComponent<StatModifiers>();

        headerStyle = new GUIStyle();
        headerStyle.fontStyle = FontStyle.Bold;

        if (randomStats) 
        {
            RandomiseStats();
        }

        FindAllModifiers();

        StartCoroutine(StatChange("meleeDamage", meleeDamage, 10, 1f));


    }

    private void OnGUI()
    {
        if(!displayDebug) return;
        
        debugWindowRect = GUI.Window(0, debugWindowRect, DrawStatsWindow, gameObject.name + " stats");
        
   }

    private void DrawStatsWindow(int windowID)
    {
        
        AddLabel("stat", "value",true);
        
        AddLabel("hp", healthPoints.ToString());
        AddLabel("ep", energyPoints.ToString());
        AddLabel("stamina", staminaPoints.ToString());
        AddLabel();
        AddLabel("melee dmg", meleeDamage.ToString());
        AddLabel("ranged dmg", rangedDamage.ToString());
        AddLabel("accuracy", accuracy.ToString());
        AddLabel();
        AddLabel("fortitude", fortitude.ToString());
        AddLabel("toughness", toughness.ToString());
        AddLabel();
        AddLabel("fire resist", fireResistance.ToString());
        AddLabel("ice resist", iceResistance.ToString());
        AddLabel("lightning resist", lightningResistance.ToString());
        AddLabel("poison resist", poisonResistance.ToString());
        AddLabel("acid resist", acidResistance.ToString());
        AddLabel();
        AddLabel("intimidation", intimidation.ToString());
        AddLabel("agility", agility.ToString());
        AddLabel("stealth", stealth.ToString());
        AddLabel("perception", perception.ToString());
        AddLabel("intelligence", intelligence.ToString());
        

        GUI.DragWindow();

        row = 0;
    }

    [Command("player-randomise-stats")]
    void RandomiseStats() 
    {
        meleeDamage = Random.Range(10, 90);
        rangedDamage = Random.Range(10, 90);
        chemicalDamage = Random.Range(10, 90);
        elementalDamage = Random.Range(10, 90);
        intelligence = Random.Range(10, 90);
        agility = Random.Range(10, 90);
        toughness = Random.Range(10, 90);

        FindAllModifiers();
    }

    
    [Command("player-set-stats")]
    void SetStats(int mdmg, int rdmg, int cdmg, int edmg, int intell, int agil, int toughn)
    {
        meleeDamage = mdmg;
        rangedDamage = rdmg;
        chemicalDamage = cdmg;
        elementalDamage = edmg;
        intelligence = intell;
        agility = agil;
        toughness = toughn;

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

    IEnumerator StatChange(string statName, int statToBuff, int buffAmount, float duration) 
    {
        Debug.Log("Buffing " + statName + " from " + statToBuff + " by " + buffAmount + " for " + duration + " seconds");
        statToBuff += buffAmount;
        FindModifier(statName, statToBuff);

        yield return new WaitForSeconds(duration);

        statToBuff -= buffAmount;
        FindModifier(statName, statToBuff);

        yield return null;
    }

    void FindAllModifiers() 
    {
        FindModifier("meleeDamage", meleeDamage);
        FindModifier("rangedDamage", rangedDamage);
        FindModifier("chemicalDamage", chemicalDamage);
        FindModifier("elementalDamage", elementalDamage);
        FindModifier("intelligence", intelligence);
        FindModifier("agility", agility);
        FindModifier("toughness", toughness);
    }

    void FindModifier(string myStat, int myStatValue) 
    {
        //Debug.Log("Called Find " + myStat + " Modifier");

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

            for (int i = 0; i < statModifiers.toughnessModifiers.Count; ++i)
            {
                if (statModifiers.toughnessModifiers[i].x <= myStatValue)
                {

                    toughnessModifier = statModifiers.toughnessModifiers[i].y;
                    
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
            for (int i = 0; i < statModifiers.intelligenceEnergyRegenModifiers.Count; ++i)
            {
                if (statModifiers.intelligenceEnergyRegenModifiers[i].x <= myStatValue)
                {

                    energyRegenModifier = statModifiers.intelligenceEnergyRegenModifiers[i].y;

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
                if (statModifiers.agilityMoveSpeedModifiers[i].x <= myStatValue)
                {

                    moveSpeedModifier = statModifiers.agilityMoveSpeedModifiers[i].y;

                }
            }
        }
    }
}
