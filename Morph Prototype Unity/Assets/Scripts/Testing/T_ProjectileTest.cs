using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class T_ProjectileTest : MonoBehaviour
{
    [SerializeField] private RadialProjectileSpawner projectileSpawner;

    // Start is called before the first frame update
    void OnEnable()
    {
        projectileSpawner.CalculateSpawnDataLocal();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            projectileSpawner.Spawn(transform);
        }
    }

    private void OnValidate()
    {
        projectileSpawner.OnValidate();
    }

    private void OnDrawGizmos()
    {
        projectileSpawner.OnDrawGizmos(transform);
    }
}
