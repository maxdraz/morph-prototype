using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stealth : MonoBehaviour
{
    //This needs a value assigned to it from the stats script
    public float maxStealth;
    public float stealthBonusWhileMoving;
    float currentStealth;
    public float finalStealthValue;
    public bool stealthMode;
    Rigidbody rb;
    private bool detected;
    float detectionAmount;
    private GameObject detectionBar;
    public RectTransform detectionBarRT;

    private Coroutine hideDetectionBarAfterTime;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.name == "Player") 
        {
            detectionBar = GameObject.Find("UI").transform.Find("Gameplay").transform.Find("Stealth Detection Bar").gameObject;
            detectionBarRT = detectionBar.GetComponentInChildren<RectTransform>();
            detectionBarRT.sizeDelta = new Vector2(2, 0);
        }

        

        detectionAmount = 0f;
        rb = GetComponentInParent<Rigidbody>();
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            stealthMode = false;
        }

        if (detectionAmount == 0 && detectionBar.activeSelf == true) 
        {
            StartCoroutine("HideDetectionBarAfterTimeCoroutine");
        }

        if (!detected)
        {
            detectionAmount -= Time.deltaTime * 2;
        }

        if (!detected) {
            if (detectionAmount > 100)
            {
                Debug.Log("You have been detected");
                detected = true;

            }
            else
            {
                detectionBarRT.sizeDelta = new Vector2(2, detectionAmount / 10);
                detectionBar.SetActive(true);
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




        currentStealth = maxStealth / (currentSpeed / 5 * Mathf.Min(1 - stealthBonusWhileMoving, 1));


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

    public void SetMaxStealth(float totalStealth)
    {
        maxStealth = totalStealth;
    }

    public float AddDetection(float detectionToAdd) 
    {
        detectionAmount += detectionToAdd;
        return detectionAmount;
    }

    private IEnumerator HideDetectionBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        detectionBar.gameObject.SetActive(false);
    }
}
