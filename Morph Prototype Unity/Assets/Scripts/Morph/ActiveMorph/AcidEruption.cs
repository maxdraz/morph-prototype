using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEruption : ActiveMorph
{
    [SerializeField] private int range;
    [SerializeField] private GameObject acidEruption;

    private Vector3 offSet = new Vector3(0, -0.25f, 0);

    //public Prerequisite[] StatPrerequisits;

    private void Start()
    {
        //WriteToPrerequisiteArray();


        //StatPrerequisits[0] = new Prerequisite("chemicalDamage", chemicalDamagePrerequisit);
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
            SpawnAcidEruption();
            return true;
        }
        return false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(testInput))
        {
            SpawnAcidEruption();
        }
    }

   


    private void SpawnAcidEruption()
    {
        RaycastToGround();

        if (hit.distance > range)
        {
            return;
        }

        else 
        {
            if (hit.point != Vector3.zero)
            {
                GameObject eruption = ObjectPooler.Instance.GetOrCreatePooledObject(acidEruption);
                eruption.transform.position = hit.point + offSet;
                eruption.GetComponent<AOE_DELAY>().StartCoroutine("TriggerActivation");
                eruption.GetComponent<AOE_DELAY>().SetDamageDealer(GetComponent<DamageHandler>());
                Debug.Log("acideruption found a target");
            }
            else 
            {
                return;
            } 
        }               
    }
}
