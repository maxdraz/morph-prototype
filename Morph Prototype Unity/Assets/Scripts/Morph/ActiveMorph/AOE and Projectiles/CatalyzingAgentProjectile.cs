using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalyzingAgentProjectile : MonoBehaviour
{
    DamageHandler damageHandler;
    public float damage;

    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DebuffHandler>().AcidStack > 0) 
        {
            collision.gameObject.GetComponent<DamageHandler>().ApplyDamage(new FireDamageData(damage), damageHandler);
        }
    }
}
