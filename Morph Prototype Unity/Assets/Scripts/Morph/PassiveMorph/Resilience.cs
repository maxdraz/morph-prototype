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

    protected override void Start() 
    {
        stats = GetComponent<Stats>();
        GenerateResistanceBoosts();
        AddToResistanceStat(resistType1, resistanceBoost1);

        if (unlockHardiness)
        {
            AddToResistanceStat(resistType2, resistanceBoost2);
        }
    }


    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }


    void GenerateResistanceBoosts()
    {

        int boost1 = Random.Range(0, 4);

        if (boost1 == 0) 
        {
            resistType1 = ("fire");

            int boost2 = Random.Range(0, 3);

            if (boost2 == 0) 
            {
                if (unlockHardiness) 
                {
                    resistType2 = ("ice");
                }
            }

            if (boost2 == 1)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("electric");
                }
            }

            if (boost2 == 2)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("poison");
                }
            }

            if (boost2 == 3)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("acid");
                }
            }
        }

        if (boost1 == 1)
        {
            resistType1 = ("ice");

            int boost2 = Random.Range(0, 3);

            if (boost2 == 0)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("fire");
                }
            }

            if (boost2 == 1)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("electric");
                }
            }

            if (boost2 == 2)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("poison");
                }
            }

            if (boost2 == 3)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("acid");
                }
            }
        }

        if (boost1 == 2)
        {
            resistType1 = ("electric");

            int boost2 = Random.Range(0, 3);

            if (boost2 == 0)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("fire");
                }
            }

            if (boost2 == 1)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("ice");
                }
            }

            if (boost2 == 2)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("poison");
                }
            }

            if (boost2 == 3)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("acid");
                }
            }
        }

        if (boost1 == 3)
        {
            resistType1 = ("poison");

            int boost2 = Random.Range(0, 3);

            if (boost2 == 0)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("fire");
                }
            }

            if (boost2 == 1)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("ice");
                }
            }

            if (boost2 == 2)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("electric");
                }
            }

            if (boost2 == 3)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("acid");
                }
            }
        }

        if (boost1 == 4)
        {
            resistType1 = ("acid");


            int boost2 = Random.Range(0, 3);

            if (boost2 == 0)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("fire");
                }
            }

            if (boost2 == 1)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("ice");
                }
            }

            if (boost2 == 2)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("electric");
                }
            }

            if (boost2 == 3)
            {
                if (unlockHardiness)
                {
                    resistType2 = ("poison");
                }
            }
        }  
    }

    private void AddToResistanceStat(string resist, float boost) 
    {
        if (resist == "fire") 
        {
            Debug.Log("adding to " + resist + " resist");
            stats.FlatResistStatChange("fire", boost);
        }

        if (resist == "ice")
        {
            Debug.Log("adding to " + resist + " resist");
            stats.FlatResistStatChange("ice", boost);   
        }

        if (resist == "electric")
        {
            Debug.Log("adding to " + resist + " resist");
            stats.FlatResistStatChange("electric", boost); 
        }

        if (resist == "poison")
        {
            Debug.Log("adding to " + resist + " resist");
            stats.FlatResistStatChange("poison", boost);
        }

        if (resist == "acid")
        {
            Debug.Log("adding to " + resist + " resist");
            stats.FlatResistStatChange("acid", boost);
        }
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
