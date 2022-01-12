using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resilience : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int resistanceBoost1;
    [SerializeField] private int resistanceBoost2;
    [SerializeField] private bool unlockHardiness = true;

    [SerializeField] private string boost1;
    [SerializeField] private string boost2;

    Stats stats;

    [SerializeField] private string[] resistArray;

    private void Start()
    {
        stats = GetComponent<Stats>();

        int boost1 = Random.Range(0, 4);

        if (boost1 == 0) 
        {
            AddToResistanceStat("fire");
        }

        if (boost1 == 1)
        {
            AddToResistanceStat("ice");
        }

        if (boost1 == 2)
        {
            AddToResistanceStat("electric");
        }

        if (boost1 == 3)
        {
            AddToResistanceStat("poison");
        }

        if (boost1 == 4)
        {
            AddToResistanceStat("acid");
        }

        RemoveAt(ref resistArray, boost1); // removes the element from the array

        if (unlockHardiness) 
        {
            int boost2 = Random.Range(0, 3);

            if (boost1 == 0)
            {
                if (resistArray[0] == "fire")
                {
                    AddToResistanceStat("fire");
                }
                else
                {
                    AddToResistanceStat("ice");
                }
            }

            if (boost1 == 1)
            {
                if (resistArray[0] == "ice")
                {
                    AddToResistanceStat("ice");
                }
                else
                {
                    AddToResistanceStat("electric");
                }
            }

            if (boost1 == 2)
            {
                if (resistArray[0] == "electric")
                {
                    AddToResistanceStat("electric");
                }
                else
                {
                    AddToResistanceStat("poison");
                }
            }

            if (boost1 == 3)
            {
                if (resistArray[0] == "poison")
                {
                    AddToResistanceStat("poison");
                }
                else
                {
                    AddToResistanceStat("acid");
                }
            }
        }  
    }

    private void AddToResistanceStat(string resist) 
    {
        if (resist == "fire") 
        {
            
        }
        if (resist == "ice")
        {

        }
        if (resist == "electric")
        {

        }
        if (resist == "poison")
        {

        }
        if (resist == "acid")
        {

        }
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
    }

    private void OnEnable()
    {

        

        StartCoroutine(AssignDamageHandlerCoroutine());
        
        stats = GetComponent<Stats>();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    
    

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            

        }

        damageHandler = null;
    }
}
