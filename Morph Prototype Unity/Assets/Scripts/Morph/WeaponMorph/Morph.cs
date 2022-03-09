using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morph : MonoBehaviour
{
    public GameObject[] morphPrerequisites;

    //This array needs to be populated with the prerequisite stats. Go to MorphLoadout.cs line 48 to see where this is needed
    public Prerequisite[] statPrerequisits;

    [SerializeField] public struct Prerequisite
    {
        public string stat;
        public int value;

        public Prerequisite(string a, int b)
        {
            stat = a;
            value = b;
            
        } 

    }
    
    public bool CheckPrerequisites(GameObject user, GameObject[] morphs, Prerequisite[] prerequisites) 
    {
        if (morphs.Length != 0) 
        {
            foreach (GameObject morph in morphs)
            {
                if (user.GetComponent<MorphLoadout>().GetMorphByName(morph.name.ToString()) == true)
                {
                    return true;
                }
                else return false;
            }
        }

        
            if (prerequisites.Length != 0) 
            {
            foreach (Prerequisite prerequisite in prerequisites)
            {
                if (user.GetComponent<Stats>().FindStatValue(prerequisite.stat) >= prerequisite.value)
                {
                    return true;
                }
                else return false;
            }
        }

        return true;
    }
}
