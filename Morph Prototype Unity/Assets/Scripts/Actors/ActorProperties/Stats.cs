using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private bool drawGizmos;
    //core
    private int healthPoints;
        //health regen?
    private int energyPoints;
        //energy regen?
    private int staminaPoints;
        //stamina regen?
    //offensive
    private int meleeDamage;
    private int rangedDamage;
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

    private void OnDrawGizmos()
    {
        if(!drawGizmos) return;
        
        
    }

    [Command("player show-stats")]
    private void DisplayStats()
    {
        drawGizmos = true;
    }
}
