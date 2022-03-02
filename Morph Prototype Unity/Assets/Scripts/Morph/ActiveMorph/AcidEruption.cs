using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEruption : ActiveMorph
{
    static int chemicalDamagePrerequisit = 25;
    [SerializeField] private int range;
    [SerializeField] private GameObject acidEruption;
    Vector3 point = new Vector3(.5f,.5f,0);
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
        int layerMask = 1 << 10;
        RaycastHit hit;
        //need to cast the ray outwards from worldspace position of the camera, so the player can aim at the ground to target where the aoe will be created
        // currently this ray casts directly out from the player character with no pitch or yaw based on the camera orientation
        if (Physics.Raycast(Camera.main.transform.position, transform.forward, out hit, range, layerMask))
        {
            GameObject eruption = ObjectPooler.Instance.GetOrCreatePooledObject(acidEruption);
            eruption.GetComponent<AcidEruptionAOE>().SetDamageDealer(GetComponent<DamageHandler>());
            eruption.transform.position = hit.point;
            Debug.Log(name + " found a target");
        }
        else 
        {
            Debug.Log(name + " did not find a target");
        }
    }
}
