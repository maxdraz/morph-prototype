using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEruptionAOE : MonoBehaviour
{
    DamageHandler damageDealer;
    CapsuleCollider collider;
    float delayPeriod;

    public float acidDamageToDeal;
    public float knockUpForce;

    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
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

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        this.damageDealer = dmgDealer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageHandler>() == true) 
        {
            other.GetComponent<DamageHandler>().ApplyDamage(new AcidDamageData(), damageDealer);
        }

        if (other.GetComponent<Rigidbody>() == true)
        {
            //Needs to be knockup
            other.GetComponent<DamageHandler>().ApplyDamage(new KnockupData(), damageDealer);
        }
    }
}
