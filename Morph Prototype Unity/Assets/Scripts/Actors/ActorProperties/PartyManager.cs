using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private GameObject startingCreature;
    private GameObject activeCreature;
    public GameObject ActiveCreature => activeCreature;
    
    [SerializeField] private List<GameObject> creatures;

    private void Awake()
    {
        creatures = new List<GameObject>()
        {
            GameObject.Instantiate(startingCreature, transform)
        };

        activeCreature = creatures[0];
        print("set active creature");
    }

    public void AddCreature(GameObject creature)
    {
        
    }
}
