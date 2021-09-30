using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stealth : MonoBehaviour
{
    //This needs a value assigned to it from the stats script
    public float maxStealth;

    float currentStealth;
    public float finalStealthValue;
    public bool stealthMode;
    Rigidbody rb;
    private bool detected;
    float detectionAmount;
    public Image detectionBar;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = detectionBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(2, 0);

        detectionAmount = 0f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!detected) 
        {
            detectionAmount -= Time.deltaTime*2;
        }

        if (!detected) {
            if (detectionAmount > 100)
            {
                Debug.Log("You have been detected");
                detected = true;

            }
            else
            {
                RectTransform rt = detectionBar.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(2, detectionAmount / 10);
            }
        }

        

        if (Input.GetKeyDown("left ctrl"))
        {
            if (!stealthMode)
            {
                stealthMode = true;
            }
            else
            {
                stealthMode = false;
            }
        }


        float currentSpeed = rb.velocity.magnitude;
        //Debug.Log(currentSpeed);




        currentStealth = maxStealth / (currentSpeed / 5);


        if (currentStealth > maxStealth * 2)
        {
            currentStealth = maxStealth * 2;
        }

        if (stealthMode)
        {
            currentStealth *= 2;

        }


        if (!stealthMode && currentSpeed == 0)
        {
            currentStealth = maxStealth;
        }

        finalStealthValue = currentStealth;

    }

    public float AddDetection(float detectionToAdd) 
    {
        detectionAmount += detectionToAdd;
        return detectionAmount;
    }
}
