using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineRush : ActiveMorph
{
    static int fortitudePrerequisit = 35;
    [SerializeField] private AdrenalineRushPrerequisiteData prerequisiteData;
    [SerializeField] private GameObject adrenalineRushParticles;
    [SerializeField] private float adrenalineBoost;

    //static Prerequisite[] StatPrerequisits;

    private void Start()
    {
        //WriteToPrerequisiteArray();
    }

    //void WriteToPrerequisiteArray()
    //{
    //    statPrerequisits = new Prerequisite[StatPrerequisits.Length];
    //
    //    for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
    //    {
    //        statPrerequisits[i] = StatPrerequisits[i];
    //        Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
    //    }
    //}

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            AdrenalineBoost();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            AdrenalineBoost();
        }
    }

    private void AdrenalineBoost()
    {
        GameObject boost = ObjectPooler.Instance.GetOrCreatePooledObject(adrenalineRushParticles);
        boost.transform.position = transform.position;
        boost.transform.parent = transform;
        GetComponent<Stamina>().AddStamina(adrenalineBoost);
    }

    public override bool CheckStatPrerequisites(MorphLoadout loadout, int statPrerequisiteArrayLength)
    {
        int positiveResults = statPrerequisiteArrayLength;

        if (!prerequisiteData)
        {
            Debug.Log(gameObject.name + " no " + GetType().ToString() + " prerequisite data assigned!");
            return false;
        }

        for (int i = 0; i <= statPrerequisiteArrayLength; i++) 
        {
            if (GetComponent<Stats>().FindStatValue(prerequisiteData.AdrenalineRushStatPrerequisites[i].stat) >= prerequisiteData.AdrenalineRushStatPrerequisites[i].value) 
            {
                positiveResults++;
            }
        }

        if (positiveResults == statPrerequisiteArrayLength)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
