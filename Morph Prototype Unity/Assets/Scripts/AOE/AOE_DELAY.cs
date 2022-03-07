using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_DELAY : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;

    public DamageHandler damageDealer;

    Collider collider;
    float delayPeriod;
    [SerializeField] private float duration;


    private void OnValidate()
    {
        OnHitEffectDataContainer.OnValidate(ref onHitEffects);
    }

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        damageDealer = dmgDealer;
    }
    void Start()
    {
        collider = GetComponent<Collider>();
        delayPeriod = GetComponent<ParticleSystem>().main.startDelay.constant;
        StartCoroutine("TriggerActivation");

    }

    IEnumerator TriggerActivation()
    {
        yield return new WaitForSeconds(delayPeriod);

        collider.enabled = true;

        yield return new WaitForSeconds(duration);

        collider.enabled = false;

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // dont collide with self
        if (other.gameObject == damageDealer.gameObject) return;
        // deal damage to enemy
        var otherDamageHandler = other.gameObject.GetComponentInChildren<DamageHandler>();

        if (otherDamageHandler)
        {
            foreach (var onHitEffectDataContainer in onHitEffects)
            {
                onHitEffectDataContainer.OnHitEffect.ApplyOnHitEffect(onHitEffectDataContainer.Data, otherDamageHandler, damageDealer);
                //print("should be applying damage");
                // otherDamageHandler.ApplyDamage(onHitEffectDataContainer.Data, damageDealer);

            }

            return;
        }
    }
}
