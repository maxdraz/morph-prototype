using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Perception : MonoBehaviour
{

    Vector3 center;
    public float perception;
    float currentPerception;
    public float globalPerceptionModifier;
    public float perceptionToApply;

    bool detecting;


    public bool showGizmo;

    VisionCone visionCone;
    SimpleScanningBehaviour simpleScanningBehaviour;

    public void Start()
    {
        if (GetComponent<VisionCone>() == true) 
        {
            visionCone = GetComponent<VisionCone>();
        }

        if (GetComponent<SimpleScanningBehaviour>() == true)
        {
            simpleScanningBehaviour = GetComponent<SimpleScanningBehaviour>();
        }

        detecting = true;
    }


    private void FixedUpdate()
    {
        currentPerception = perception;
        center = transform.position;




        Collider[] hitColliders = Physics.OverlapSphere(center, currentPerception / globalPerceptionModifier);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stealth>() == true && detecting)
            {

                float enemyStealthValue = hitCollider.gameObject.GetComponent<Stealth>().finalStealthValue;
                float dist = Vector3.Distance(transform.position, hitCollider.transform.position);

                if (visionCone.playerInSight == true)
                {
                    perceptionToApply = currentPerception / (Mathf.Sqrt(dist)/2);
                    //Debug.Log("Percieving with LoS, " + transform.name + " is trying to detect you with " + perceptionToApply + " perception against your " + enemyStealthValue + " stealth");
                }
                else
                {
                    perceptionToApply = currentPerception / (Mathf.Sqrt(dist));
                    //Debug.Log("Percieving without LoS, " + transform.name + " is trying to detect you with " + perceptionToApply + " perception against your " + enemyStealthValue + " stealth");
                }



                //Enemy is being detected quickly
                if (perceptionToApply > enemyStealthValue * 3)
                {
                    if (detecting) 
                    {
                        //hitCollider.gameObject.GetComponent<Stealth>().AddDetection(3f);
                        simpleScanningBehaviour.StartCoroutine("Investigate", hitCollider.gameObject.transform.position);
                        //Debug.Log("You are being detected quickly");

                    }


                }
                else
                {
                    //Enemy is being detected slowly
                    if (perceptionToApply > enemyStealthValue)
                    {
                        if (detecting)
                        {
                            //hitCollider.gameObject.GetComponent<Stealth>().AddDetection(1f);
                            //Debug.Log("You are being detected slowly");
 
                        }
                    }
                }
            }
        }
    }
    
    

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            float radius = currentPerception/globalPerceptionModifier;
            center = this.gameObject.transform.position;
            Gizmos.color = new Color(1, 1, 1, 0.5f);
            Gizmos.DrawSphere(center, radius);
        }
    }
}
