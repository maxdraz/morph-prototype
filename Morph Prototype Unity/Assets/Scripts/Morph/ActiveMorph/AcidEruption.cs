using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEruption : ActiveMorph
{
    static int chemicalDamagePrerequisit = 25;
    [SerializeField] private int range;
    [SerializeField] private GameObject acidEruption;

    private Vector3 offSet = new Vector3(0, -0.25f, 0);

    public Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),

    };

   

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
        //Debug.DrawRay(Camera.main.transform.position, transform.TransformDirection(Vector3.back), Color.yellow, range * 10));
        //Camera.main.ScreenToWorldPoint(point)
        //Debug.DrawRay(Camera.main.transform.position, transform.forward, Color.blue);

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
                eruption.GetComponent<AOE_DELAY>().SetDamageDealer(GetComponent<DamageHandler>());
                eruption.transform.position = hit.point + offSet;
                Debug.Log("acideruption found a target");
            }
            else 
            {
                return;
            } 
        }               
    }
}
