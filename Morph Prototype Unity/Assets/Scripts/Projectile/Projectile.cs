using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float startSpeed = 100;
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;
    
    private DamageHandler damageDealer;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        
        rb.velocity = Vector3.zero;

        float totalSpeed = startSpeed;
        if (damageDealer)
        {
            var damageDearlerRigidbody = damageDealer.GetComponentInParent<Rigidbody>();
            if (damageDearlerRigidbody)
            {
                var velocity = damageDearlerRigidbody.velocity;
                var speedXZ = new Vector3(velocity.x, 0, velocity.z).magnitude;
                totalSpeed += speedXZ;
            }
        }

        rb.AddForce(transform.forward * totalSpeed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        OnHitEffectDataContainer.OnValidate(ref onHitEffects);
    }

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        this.damageDealer = dmgDealer;
    }
}
