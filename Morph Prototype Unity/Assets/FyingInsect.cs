using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FyingInsect : MonoBehaviour
{
    Rigidbody rb;
    Vector3 vector;
    public float minPeriod;
    public float maxPeriod;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("RandomHeading");
    }

    IEnumerator RandomHeading()
    {
        float period = Random.Range(minPeriod, maxPeriod);
        yield return new WaitForSeconds(period);


        RandomVector(-5,5);

        StartCoroutine("RandomHeading");

        yield return null;
    }
    
    Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);

        vector = new Vector3(x,y,z);

        rb.velocity = vector;
        transform.LookAt(vector);
        return new Vector3(x, y, z);
    }
}
