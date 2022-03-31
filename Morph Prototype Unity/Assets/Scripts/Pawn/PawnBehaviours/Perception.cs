using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Perception : MonoBehaviour
{

    Vector3 center;
    public float maxPerception;
    public float perceptionModifier;
    private float currentPerception;
    float globalPerceptionModifier;    
    public float perceptionToApply;

    bool detecting;

    public bool showGizmo;


    public float CurrentPerception => currentPerception;

    public void Start()
    {

        detecting = true;
        StartCoroutine("PerceptionCheck");
    }

    public void SetMaxPerception(float totalPerception)
    {
        maxPerception = totalPerception;
    }

    IEnumerator PerceptionCheck()
    {
        yield return new WaitForSeconds(.5f);

        currentPerception = maxPerception * (1 + perceptionModifier);
        center = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, currentPerception / globalPerceptionModifier);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stealth>() == true && hitCollider.gameObject != gameObject)
            {

                float enemyStealthValue = hitCollider.gameObject.GetComponent<Stealth>().finalStealthValue;
                float dist = Vector3.Distance(transform.position, hitCollider.transform.position);

                    //We want to check to see if the sneaking enemy is within the field of view of the main camera, if so the perceptionToApply is doubled
                    //if (visionCone.playerInSight == true)
                    //{
                        perceptionToApply = currentPerception / (Mathf.Sqrt(dist) / 2);
                    //}
                    //else
                    //{
                    //    perceptionToApply = currentPerception / Mathf.Sqrt(dist);
                    //}

                //Enemy is being detected quickly
                if (perceptionToApply > enemyStealthValue * 3)
                {
                    hitCollider.gameObject.GetComponent<Stealth>().AddDetection(3f);
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
        StartCoroutine("PerceptionCheck");
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
