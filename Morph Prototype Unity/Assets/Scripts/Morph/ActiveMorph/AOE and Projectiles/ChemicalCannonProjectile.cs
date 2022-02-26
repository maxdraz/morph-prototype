using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCannonProjectile : MonoBehaviour
{
    [SerializeField] private GameObject chemicalCannonExplosion;

   

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = ObjectPooler.Instance.GetOrCreatePooledObject(chemicalCannonExplosion);
        explosion.transform.position = transform.position;
        ObjectPooler.Instance.Recycle(gameObject);
    }
}
