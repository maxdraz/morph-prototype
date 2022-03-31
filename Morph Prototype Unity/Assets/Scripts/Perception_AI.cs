using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception_AI : MonoBehaviour
{
    Vector3 center;
    public float maxPerception;
    public float perceptionModifier;
    private float currentPerception;
    float globalPerceptionModifier;
    public float perceptionToApply;

    bool detecting;

    public bool showGizmo;

    VisionCone visionCone;
    SimpleScanningBehaviour simpleScanningBehaviour;

    public float CurrentPerception => currentPerception;

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


            if (hitCollider.gameObject.GetComponent<Stealth>() == true && detecting)
            {

                float enemyStealthValue = hitCollider.gameObject.GetComponent<Stealth>().finalStealthValue;
                float dist = Vector3.Distance(transform.position, hitCollider.transform.position);

                
                    if (visionCone.playerInSight == true)
                    {
                        perceptionToApply = currentPerception / (Mathf.Sqrt(dist) / 2);
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
                        simpleScanningBehaviour.StartCoroutine("Investigate", hitCollider.gameObject.transform.position);
                        //hitCollider.gameObject.GetComponent<Stealth>().AddDetection(3f);
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
        StartCoroutine("PerceptionCheck");
    }





    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            float radius = currentPerception / globalPerceptionModifier;
            center = this.gameObject.transform.position;
            Gizmos.color = new Color(1, 1, 1, 0.5f);
            Gizmos.DrawSphere(center, radius);
        }
    }
}
