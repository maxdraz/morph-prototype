using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PartyManager))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    private PartyManager party;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        GetComponent<MeshRenderer>().enabled = false;
        party = GetComponent<PartyManager>();
    }

    public GameObject GetActiveCreature()
    {
        return party.ActiveCreature;
    }
   
   
}
