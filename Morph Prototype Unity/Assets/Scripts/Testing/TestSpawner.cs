using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    ObjectPooler pooler;
    int numberSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left ctrl")) 
        {
            numberSpawned++;
            GameObject spawnedObject = pooler.GetOrCreatePooledObject(objectToSpawn);
            spawnedObject.GetComponent<DamageReport>().damage += (25 * numberSpawned); 
        
        }
    }
}
