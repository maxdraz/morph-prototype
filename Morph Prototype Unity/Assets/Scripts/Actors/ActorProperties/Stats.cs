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
    [SerializeField] private Rect debugWindowRect;
    [SerializeField] private float rowOffset;
    [SerializeField] private float colOffset;
    [SerializeField] private Vector2 origin;
    [SerializeField] private Vector2 labelDimensions;
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

    private void Reset()
    {
        debugWindowRect = new Rect(20, 20, 100, 100);
    }

    private void Awake()
    {
        headerStyle = new GUIStyle();
        headerStyle.fontStyle = FontStyle.Bold;

        if (randomStats) 
        {
            meleeDamage = Random.Range(10,90);
            rangedDamage = Random.Range(10, 90);
            chemicalDamage = Random.Range(10, 90);
            elementalDamage = Random.Range(10, 90);
            intelligence = Random.Range(10, 90);
            agility = Random.Range(10, 90);
            toughness = Random.Range(10, 90);

        }
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
}
