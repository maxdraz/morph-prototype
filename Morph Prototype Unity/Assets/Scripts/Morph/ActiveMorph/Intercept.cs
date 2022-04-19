using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercept : ActiveMorph
{
     [SerializeField] private InterceptPrerequisiteData prerequisiteData;


    DamageHandler damageHandler;
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    Rigidbody rb;
    bool goingToImpact;
    [SerializeField] private float staminaGain;
    bool gainedStamina;
    [SerializeField] private float range;

    //static Prerequisite[] StatPrerequisits;

    private void Start()
    {
        //WriteToPrerequisiteArray();
    }

    //void WriteToPrerequisiteArray()
    //{
    //    statPrerequisits = new Prerequisite[StatPrerequisits.Length];
    //
    //    for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
    //    {
    //        statPrerequisits[i] = StatPrerequisits[i];
    //        Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
    //    }
    //}

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        goingToImpact = false;
    }



    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void Update()
    {
        if (cooldown.JustCompleted) 
        {
            gainedStamina = false;
        }

        if (Input.GetKeyDown(testInput)) 
        {
            Leap();
        }
    }

    private void Leap()
    {
        if (rb == null)
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        rb.AddForce(0, 1 * verticalForce, 0, ForceMode.Impulse);

        rb.AddForce(0, 0, 1 * horizontalForce, ForceMode.Impulse);

        Debug.Log("Leaping");

        goingToImpact = true;
    }

    private void Impact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Stats>() && hitCollider.gameObject != gameObject)
            {
                BroadcastMessage("AddStamina",staminaGain);
                gainedStamina = true;
            }
        }

        goingToImpact = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (goingToImpact)
            {
                Impact();
            }
        }
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {

        }

        damageHandler = null;
    }
}
