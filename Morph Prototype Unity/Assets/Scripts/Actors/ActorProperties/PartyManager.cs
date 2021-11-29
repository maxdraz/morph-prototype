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
        party ??= new List<GameObject>();
        
       
        if (transform.childCount > 0 && transform.GetChild(0))
        {
            List<GameObject> children = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i).gameObject);
            }
            AddCreaturesToParty(children);
        }
        else if(startingCreature)
        {
            AddCreatureToParty(GameObject.Instantiate(startingCreature, transform));
        }

        if (party.Count > 0) 
            SetActiveCreature(party[0]);

        print("set active creature");
    }

    public void AddCreatureToParty(GameObject creature)
    {
        party.Add(creature);
    }
    
    public void AddCreaturesToParty(List<GameObject> creatures)
    {
        foreach (var creature in creatures)
        {
            party.Add(creature);
        }
    }

    public void SetActiveCreature(GameObject creature)
    {
        for (int i = 0; i < party.Count; i++)
        {
            var currenCreature = party[i];
            if (creature == currenCreature)
            {
                activeCreature = currenCreature;
                activeCreature.SetActive(true);
                continue;
            }

            currenCreature.SetActive(false);
        }
    }
}
