using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_DOT : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;

    private DamageHandler damageDealer;

    private void Start()
    {
        StartCoroutine("DOT");
    }

    private void OnValidate()
    {
        OnHitEffectDataContainer.OnValidate(ref onHitEffects);
    }

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        this.damageDealer = dmgDealer;
    }

    IEnumerator DOT() 
    {
        yield return new WaitForSeconds(1);

        AOEEffect();

        StartCoroutine("DOT");

        yield return null;
    }

    void AOEEffect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);

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
