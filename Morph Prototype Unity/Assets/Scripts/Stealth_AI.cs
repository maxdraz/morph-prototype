using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth_AI : MonoBehaviour
{
    public int maxStealth;
    public float stealthModifierWhileMoving;
    public int flatStealthModifier;
    public float percentageStealthModifier;
    int currentStealth;
    public int finalStealthValue;
    public bool stealthMode;
    Velocity velo;
    private bool detected;
    float detectionAmount;



    // Start is called before the first frame update
    void Start()
    {
        detectionAmount = 0f;
        velo = GetComponentInParent<Velocity>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!detected)
        {
            detectionAmount -= Time.deltaTime * 2;
        }

        if (!detected)
        {
            if (detectionAmount > 100)
            {
                Debug.Log(transform.name + " has been detected");
                //detected bool mean that your detectionAmount has exceeded 100, which means you have been detected. The AI creature should now become visible to the player
                detected = true;
            }
        }

        float currentSpeed = 0;
        if (velo)
        {
            currentSpeed = velo.CurrentVelocity.magnitude;

            if (velo.CurrentVelocity.magnitude > 0)
            {
                currentStealth = Mathf.RoundToInt((maxStealth + flatStealthModifier) / ((1 + currentSpeed) * (1 + stealthModifierWhileMoving)) * (1 + percentageStealthModifier));
            }
            else
            {
                currentStealth = Mathf.RoundToInt((maxStealth + flatStealthModifier) * (1 + percentageStealthModifier));
            }
        }
        

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

    public void SetMaxStealth(int totalStealth)
    {
        maxStealth = totalStealth;
    }

    public float AddDetection(float detectionToAdd)
    {
        detectionAmount += detectionToAdd;
        return detectionAmount;
    }
}
