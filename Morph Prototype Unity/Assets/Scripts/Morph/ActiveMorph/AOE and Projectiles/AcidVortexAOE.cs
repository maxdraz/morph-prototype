using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVortexAOE : MonoBehaviour
{
    [SerializeField] private PullTowardsData pullTowardsData;
    [SerializeField] private KnockbackData knockbackData;
    [SerializeField] private List<Rigidbody> rigidbodies;
    private bool shouldPull;

    private void OnEnable()
    {
        StartCoroutine(ExecuteAOECoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        rigidbodies.Clear();
    }

    private void FixedUpdate()
    {
        if(rigidbodies.Count <= 0) return;

        foreach (var rb in rigidbodies)
        {
            if (shouldPull)
            {
                var toTarget = pullTowardsData.PullTowardsDebuff.Target.position - rb.position;
                rb.AddForce(toTarget.normalized * pullTowardsData.PullTowardsDebuff.PullForce, pullTowardsData.PullTowardsDebuff.ForceMode);
            }
           
        }
    }

    public void Initialize(DamageHandler dmgDealer)
    {
        
    }

    private IEnumerator ExecuteAOECoroutine()
    {
        shouldPull = true;
        yield return new WaitForSeconds(3);
        shouldPull = false;
    }

    private void OnValidate()
    {
        // foreach (var onHitEffectDataContainer in onPullEffects)
        // {
        //     onHitEffectDataContainer.OnValidate();
        // }
        //
        // foreach (var onHitEffectDataContainer in onPushEffects)
        // {
        //     onHitEffectDataContainer.OnValidate();
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
       // var dmgTaker = other.GetComponentInChildren<DamageHandler>();
       // if(!dmgTaker || dmgTaker.gameObject == gameObject) return;
        
        //OnHitEffectDataContainer.ApplyOnHitEffects(ref onPullEffects, dmgTaker, damageDealer);
        if(other.transform.root == transform.root) return;
        print(other.name);
        var rb = other.GetComponentInParent<Rigidbody>();
        if(rb)
            rigidbodies.Add(rb);
        
    }
    
    private void OnTriggerExit(Collider other)
        {
           // var dmgTaker = other.GetComponentInChildren<DamageHandler>();
           // if(!dmgTaker || dmgTaker.gameObject == gameObject) return;
            
            //OnHitEffectDataContainer.ApplyOnHitEffects(ref onPullEffects, dmgTaker, damageDealer);
            if(rigidbodies.Count <= 0 || other.transform.root == transform.root) return;

            for (int i = 0; i < rigidbodies.Count; i++)
            {
                var rb = rigidbodies[i];
                if (rb.transform.root == other.transform.root)
                {
                    rigidbodies.RemoveAt(i);
                    i--;
                }
            }
        }
}
