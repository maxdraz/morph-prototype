using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercept : ActiveMorph
{
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    private Rigidbody rb;
    private bool goingToImpact;
    [SerializeField] private float staminaGain;
    private bool gainedStamina;
    [SerializeField] private float range;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        rb = GetComponentInParent<Rigidbody>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        goingToImpact = false;
    }

    protected override void Update()
    {
        base.Update();
        
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
        if(!rb) return;
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
}
