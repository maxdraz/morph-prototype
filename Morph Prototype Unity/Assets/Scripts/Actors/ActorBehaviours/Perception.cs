using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{

    Vector3 center;
    public float perception;
    float currentPerception;
    public float myPerceptionModifier;
    public float perceptionToApply;

    bool detecting;
    float timeBetweenPerceptions = .5f;

    //NEED A CURRENT DETECTIONS FOR EACH ENEMY IN RANGE. CURRENTLY ANY 3 ENEMY DETECTIONS WILL AGGRO THE CREATURE
    public int currentDetections;
    public float detectionDecayTime = 10f;
    int detectionsBeforeAggro = 3;

    public bool showGizmo;

    VisionCone visionCone;

    public void Start()
    {
        if (GetComponent<VisionCone>() == true) 
        {
            visionCone = GetComponent<VisionCone>();
        }

       
        detecting = true;
        //StartCoroutine("PerceptionLoop");
    }


    private void FixedUpdate()
    {
        currentPerception = perception;
        center = transform.position;




        Collider[] hitColliders = Physics.OverlapSphere(center, currentPerception / myPerceptionModifier);

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
                        hitCollider.gameObject.GetComponent<Stealth>().AddDetection(3f);
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
                            hitCollider.gameObject.GetComponent<Stealth>().AddDetection(1f);
                            //Debug.Log("You are being detected slowly");


                            if (currentDetections == 3)
                            {
                                //Enemy has been detected, this entity has now entered a combat state
                                Debug.Log("You have been spotted prepare for battle!");
                                detecting = false;

                            }
                            
                        }

                    }

                }
            }
        }
    }
    IEnumerator PerceptionLoop()
    {
        

        yield return new WaitForSeconds(timeBetweenPerceptions);
        currentPerception = perception;
        center = transform.position;

        

        
        Collider[] hitColliders = Physics.OverlapSphere(center, currentPerception/myPerceptionModifier);

        foreach (var hitCollider in hitColliders)
        {
            

            if (hitCollider.gameObject.GetComponent<Stealth>() == true && detecting)
            {


                float enemyStealthValue = hitCollider.gameObject.GetComponent<Stealth>().finalStealthValue;
                float dist = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (visionCone.playerInSight == true)
                {
                    perceptionToApply = currentPerception / (dist / 16);
                    Debug.Log("Percieving with LoS, " + transform.name + " is trying to detect you with " + perceptionToApply + " perception against your " + enemyStealthValue + " stealth");
                }
                else
                {
                    perceptionToApply = currentPerception / (dist / 8);
                    Debug.Log("Percieving without LoS, " + transform.name + " is trying to detect you with " + perceptionToApply + " perception against your " + enemyStealthValue + " stealth");
                }
                
                

                //Enemy is identified immediately
                if (perceptionToApply > enemyStealthValue * 3)
                {
                    //Enemy has been detected, this entity has now entered a combat state
                    currentDetections = detectionsBeforeAggro;
                    Debug.Log("You have been spotted prepare for battle!");
                    detecting = false;

                }
                else
                {
                    //Enemy has been partially detected
                    if (perceptionToApply > enemyStealthValue)
                    {
                        if (detecting)
                        {
                            currentDetections++;
                            Debug.Log("You have been detected " + currentDetections + " times");

                            if (currentDetections == 3)
                            {
                                //Enemy has been detected, this entity has now entered a combat state
                                Debug.Log("You have been spotted prepare for battle!");
                                detecting = false;

                            }
                            else
                            {
                                StartCoroutine("DetectionsDecay");
                            }
                        }

                    }

                }
            }
        }

        StartCoroutine("PerceptionLoop");
        yield return null;
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            float radius = currentPerception/myPerceptionModifier;
            center = this.gameObject.transform.position;
            Gizmos.color = new Color(1, 1, 1, 0.5f);
            Gizmos.DrawSphere(center, radius);
        }
    }
}
