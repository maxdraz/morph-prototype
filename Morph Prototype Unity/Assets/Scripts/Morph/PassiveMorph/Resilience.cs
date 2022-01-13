using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resilience : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float resistanceBoost1;
    [SerializeField] private float resistanceBoost2;
    [SerializeField] private bool unlockHardiness = true;
    [SerializeField] private string resistType1;
    [SerializeField] private string resistType2;

    Stats stats;

    [SerializeField] private string[] resistArray;

    private void Start()
    {
        stats = GetComponent<Stats>();

        int boost1 = Random.Range(0, 4);

        if (boost1 == 0) 
        {
            resistType1 = resistArray[0];
        }

        if (boost1 == 1)
        {
            resistType1 = resistArray[1];
        }

        if (boost1 == 2)
        {
            resistType1 = resistArray[2];
        }

        if (boost1 == 3)
        {
            resistType1 = resistArray[3];
        }

        if (boost1 == 4)
        {
            resistType1 = resistArray[4];
        }

        RemoveAt(ref resistArray, boost1); // removes the element from the array

        

        int boost2 = Random.Range(0, 3);

        if (boost2 == 0)
        {
            resistType2 = resistArray[0];
        }

        if (boost1 == 1)
        {
            resistType2 = resistArray[1];
        }

        if (boost1 == 2)
        {
            resistType2 = resistArray[2];
        }

        if (boost1 == 3)
        {
            resistType2 = resistArray[3];
        }
    }

    private void AddToResistanceStat(string resist, float boost) 
    {
        if (resist == "fire") 
        {
            
            
                stats.FlatResistStatChange("fire", boost);
            
        }
        if (resist == "ice")
        {
            
                stats.FlatResistStatChange("ice", boost);
            
        }
        if (resist == "electric")
        {
            
                stats.FlatResistStatChange("electric", boost);
            
        }
        if (resist == "poison")
        {
            
                stats.FlatResistStatChange("poison", boost);
            
        }
        if (resist == "acid")
        {
            
                stats.FlatResistStatChange("acid", boost);
            
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
        stats = GetComponent<Stats>();


        AddToResistanceStat(resistType1.ToString(), resistanceBoost1);

        if (unlockHardiness) 
        {
            AddToResistanceStat(resistType2.ToString(), resistanceBoost2);
        }

        StartCoroutine(AssignDamageHandlerCoroutine());
        
    }

    private void OnDisable()
    {
        AddToResistanceStat(resistType1.ToString(), -resistanceBoost1);

        if (unlockHardiness)
        {
            AddToResistanceStat(resistType2.ToString(), -resistanceBoost2);
        }

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
