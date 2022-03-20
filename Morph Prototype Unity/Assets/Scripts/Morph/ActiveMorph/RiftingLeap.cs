using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftingLeap : ActiveMorph
{
    static int agilityDamagePrerequisite = 20;
    static int toughnessPrerequisite = 30;


    DamageHandler damageHandler;
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    Rigidbody rb;
    bool goingToImpact;
    Vector3 landingParticlesOffset = new Vector3(0, -1, 0);
    [SerializeField] private GameObject impactParticles;


    [SerializeField] private float physicalDamage;
    [SerializeField] private float knockUpForce;
    [SerializeField] private float range;

    public Prerequisite[] StatPrerequisits;

    private void Start()
    {
        WriteToPrerequisiteArray();
    }

    void WriteToPrerequisiteArray()
    {
        statPrerequisits = new Prerequisite[StatPrerequisits.Length];

        for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
        {
            statPrerequisits[i] = StatPrerequisits[i];
            Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
        }
    }

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
        GameObject landingParticles = ObjectPooler.Instance.GetOrCreatePooledObject(impactParticles);
        landingParticles.transform.position = transform.position + landingParticlesOffset;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Stats>() && hitCollider.gameObject != gameObject)
            {
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PhysicalDamageData (physicalDamage),damageHandler);
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new KnockupData(knockUpForce), damageHandler);
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
