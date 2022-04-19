using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterCreature : ActiveMorph
{
    [SerializeField] private CraterCreaturePrerequisiteData prerequisiteData;


    DamageHandler damageHandler;
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    Rigidbody rb;
    bool goingToImpact;


    [SerializeField] private float physicalDamage;
    [SerializeField] private float range;
    public GameObject crater;

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
        rb = GetComponentInParent<Rigidbody>();
        goingToImpact = false;
    }



    private void OnDisable()
    {

        UnsubscribeFromEvents();


    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            Leap();
        }
    }

    private void Leap()
    {
        rb.AddForce(Vector3.up, ForceMode.Impulse);

        rb.AddForce(Vector3.forward, ForceMode.Impulse);

        goingToImpact = true;
    }

    private void Impact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        Instantiate(crater, transform.position, transform.rotation);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Stats>() && hitCollider.gameObject != gameObject)
            {
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PhysicalDamageData(physicalDamage), damageHandler);
                
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
