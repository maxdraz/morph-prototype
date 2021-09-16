using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    Vector3 center;
    public float perception;
    public float currentPerception;
    public float myPerceptionModifier;
    public float perceptionToApply;

    bool detecting;
    float timeBetweenPerceptions = 3f;

    //NEED A CURRENT DETECTIONS FOR EACH ENEMY IN RANGE. CURRENTLY ANY 3 ENEMY DETECTIONS WILL AGGRO THE CREATURE
    int currentDetections;
    float detectionDecayTime = 10f;
    int detectionsBeforeAggro = 3;

    public bool showGizmo;

    public void Start()
    {
        detecting = true;
        StartCoroutine("PerceptionLoop");
    }

    IEnumerator PerceptionLoop()
    {
        yield return new WaitForSeconds(timeBetweenPerceptions);
        currentPerception = perception;
        center = transform.position;

        //Debug.Log("Percieving");
        Collider[] hitColliders = Physics.OverlapSphere(center, currentPerception);

        foreach (var hitCollider in hitColliders)
        {
            

            if (hitCollider.gameObject.GetComponent<Stealth>() == true && detecting)
            {


                float enemyStealthValue = hitCollider.gameObject.GetComponent<Stealth>().finalStealthValue;
                float dist = Vector3.Distance(transform.position, hitCollider.transform.position);
                perceptionToApply = currentPerception / (1 + dist / 100);

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
                        }

                    }

                }
            }
        }
        StartCoroutine("PerceptionLoop");
        yield return null;
    }

    

    IEnumerator DetectionsDecay()
    {
        yield return new WaitForSeconds(detectionDecayTime);
        if (currentDetections < detectionsBeforeAggro && detecting) 
        {
            currentDetections--;
        }

        

        if (currentDetections > 0)
        {
            StartCoroutine("DetectionsDecay");
        }
        else
        {
            yield return null;
        }
    }
}
