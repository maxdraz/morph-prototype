using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private GameObject startingCreature;
    private GameObject activeCreature;
    public GameObject ActiveCreature => activeCreature;
    
    [SerializeField] private List<GameObject> party;

    private void Awake()
    {
        party = new List<GameObject>()
        {
            GameObject.Instantiate(startingCreature, transform)
        };

        activeCreature = party[0];
        print("set active creature");
    }

    public void AddCreature(GameObject creature)
    {
        
    }
}
