using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEruptionProjectile : MonoBehaviour
{
    DamageHandler damageHandler;
    SphereCollider collider;
    float delayPeriod;

    public float acidDamageToDeal;
    public float knockUpForce;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        delayPeriod = GetComponent<ParticleSystem>().main.startDelay.constant;
        StartCoroutine("TriggerActivation");
    }

    IEnumerator TriggerActivation() 
    {
        yield return new WaitForSeconds(delayPeriod);

        collider.enabled = true;

        yield return new WaitForSeconds(.2f);

        collider.enabled = false;

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageHandler>() == true) 
        {
            other.GetComponent<DamageHandler>().ApplyDamage(new AcidDamageData(),damageHandler);
        }

        if (other.GetComponent<Rigidbody>() == true)
        {
            //Needs to be knockup
            other.GetComponent<DamageHandler>().ApplyDamage(new KnockupData(), damageHandler);
        }
    }
}
