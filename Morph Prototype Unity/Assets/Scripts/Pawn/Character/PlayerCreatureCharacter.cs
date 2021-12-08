using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PartyManager))]
public class PlayerCreatureCharacter : CreatureCharacter
{
    public static PlayerCreatureCharacter Instance;

    public PartyManager PartyManager;
    public MorphLoadout CurrentCreatureMorphLoadout => PartyManager.ActiveCreature.GetComponent<MorphLoadout>();

    public bool CanAcceptInput;

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

        CanAcceptInput = false;

        PartyManager = GetComponent<PartyManager>();

    }
}
