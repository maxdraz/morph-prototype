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
    private Vector2 baseHealthPoints = new Vector2(500, 800);
    private int healthRegen;
    [SerializeField] private int maxHealth;
    private Vector2 baseEnergyPoints = new Vector2(100, 200);
    private int energyRegen;
    private Vector2 baseStaminaPoints = new Vector2(300, 500);
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

    [SerializeField] private float maxEnergyModifier;
    [SerializeField] private float cooldownModifier;

    [SerializeField] private float attackSpeedModifier;
    [SerializeField] private float moveSpeedModifier;

    [SerializeField] private float maxStaminaModifier;

    StatModifiers statModifiers;
    CombatResources combatResources;
    
    //public interface
    public float MaxHealth => maxHealth;
    public float ElementalDamageModifier => elementalDamageModifier;
    public float ChemicalDamageModifier => chemicalDamageModifier;
    public float MeleeDamageModifier => meleeDamageModifier;
    public float PoisonResistance => poisonResistance;
    public float ToughnessModifier => toughnessModifier;

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
 
        combatResources = GetComponentInParent<CombatResources>();
        headerStyle = new GUIStyle();
        headerStyle.fontStyle = FontStyle.Bold;

        if (randomStats) 
        {
            RandomiseStats();
        }

        FindAllModifiers();

        StartCoroutine(StatChange("meleeDamage", meleeDamage, 10, 1f));

        PrepareCombatResources();
    }

    private void OnGUI()
    {
        if(!displayDebug) return;
        
        debugWindowRect = GUI.Window(0, debugWindowRect, DrawStatsWindow, gameObject.name + " stats");
        
   }
 

    private void PrepareCombatResources() 
    {
        float healthPointsToRandomize;
        float energyPointsToRandomize;
        float staminaPointsToRandomize;

        healthPointsToRandomize = Random.Range(baseHealthPoints.x, baseHealthPoints.y);
        energyPointsToRandomize = Random.Range(baseEnergyPoints.x, baseEnergyPoints.y) * (1 + maxEnergyModifier);
        staminaPointsToRandomize = Random.Range(baseStaminaPoints.x, baseStaminaPoints.y) * (1 + maxStaminaModifier);

        int healthPointsToSend = Mathf.RoundToInt(healthPointsToRandomize);
        int emergyPointsToSend = Mathf.RoundToInt(energyPointsToRandomize);
        int staminaPointsToSend = Mathf.RoundToInt(staminaPointsToRandomize);

        if(combatResources) combatResources.SetCombatRescources(healthPointsToSend, emergyPointsToSend, staminaPointsToSend); ;
    }

    private void DrawStatsWindow(int windowID)
    {
        
        AddLabel("stat", "value",true);
        
        AddLabel("hp", baseHealthPoints.ToString());
        AddLabel("ep", baseEnergyPoints.ToString());
        AddLabel("stamina", baseStaminaPoints.ToString());
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
        fortitude = Random.Range(10, 90);
        perception = Random.Range(10, 90);
        intimidation = Random.Range(10, 90);
        stealth = Random.Range(10, 90);



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
      //  Debug.Log("Buffing " + statName + " from " + statToBuff + " by " + buffAmount + " for " + duration + " seconds");
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
        FindModifier("fortitude", fortitude);

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
                if (statModifiers.toughnessDamageReductionModifiers[i].x <= myStatValue)
                {

                    maxStaminaModifier = statModifiers.fortitudeMaxStaminaModifiers[i].y;

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
                if (statModifiers.intelligenceMaxEnergyModifiers[i].x <= myStatValue)
                {

                    maxEnergyModifier = statModifiers.intelligenceMaxEnergyModifiers[i].y;

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
