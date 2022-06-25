using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{


    private void OnTriggerStay(Collider col)
    {
        if (col.transform.tag == "Ground")
        {
            GetComponentInParent<Stamina>().grounded = true;
            GetComponentInParent<Stamina>().StartCoroutine("RegenTimer");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Ground")
        {
            GetComponentInParent<Stamina>().grounded = false;
            GetComponentInParent<Stamina>().StopCoroutine("RegenTimer");
        }
    }
}
