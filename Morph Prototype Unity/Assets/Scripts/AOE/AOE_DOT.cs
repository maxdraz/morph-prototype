using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_DOT : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;

    private DamageHandler damageDealer;
    [SerializeField] private float timeBetweenEffects;
    [SerializeField] private float duration;
    [SerializeField] private float radius;

    private void Start()
    {
        StartCoroutine("DOT");

        if (duration > 0)
        {
            StartCoroutine("Duration");
        }
    }

    private void OnValidate()
    {
        OnHitEffectDataContainer.OnValidate(ref onHitEffects);
    }

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        damageDealer = dmgDealer;
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);

        StopCoroutine("DOT");

        yield return null;
    }

    IEnumerator DOT() 
    {
        yield return new WaitForSeconds(timeBetweenEffects);

        AOEEffect();

        StartCoroutine("DOT");

        yield return null;
    }

    void AOEEffect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == damageDealer.gameObject) return;
            // deal damage to enemy
            var otherDamageHandler = hitCollider.gameObject.GetComponentInChildren<DamageHandler>();

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
}
