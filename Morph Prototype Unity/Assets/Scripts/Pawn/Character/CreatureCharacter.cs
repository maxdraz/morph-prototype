using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCharacter : MonoBehaviour
{
    private PartyManager partyManager;
    
    public PartyManager PartyManager => partyManager;
    
    protected virtual void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
        partyManager = GetComponent<PartyManager>();
    }
}
