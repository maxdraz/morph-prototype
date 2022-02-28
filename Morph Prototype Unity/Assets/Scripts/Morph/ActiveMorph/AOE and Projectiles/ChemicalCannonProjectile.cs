using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCannonProjectile : MonoBehaviour
{
    [SerializeField] private GameObject chemicalCannonExplosion;
    public DamageHandler source;
   

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = ObjectPooler.Instance.GetOrCreatePooledObject(chemicalCannonExplosion);
        explosion.transform.position = transform.position;
        explosion.GetComponent<AOE>().SetDamageDealer(source);
        ObjectPooler.Instance.Recycle(gameObject);
    }
}
