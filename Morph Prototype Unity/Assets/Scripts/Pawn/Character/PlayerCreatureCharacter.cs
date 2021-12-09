using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PartyManager))]
public class PlayerCreatureCharacter : CreatureCharacter
{
    public static PlayerCreatureCharacter Instance;

    [HideInInspector] public PartyManager PartyManager;
    public MorphLoadout CurrentCreatureMorphLoadout => PartyManager.ActiveCreature.GetComponent<MorphLoadout>();

    public bool CanAcceptInput = true;

    protected override void Awake()
    {
        base.Awake();
        
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        PartyManager = GetComponent<PartyManager>();

    }
}
