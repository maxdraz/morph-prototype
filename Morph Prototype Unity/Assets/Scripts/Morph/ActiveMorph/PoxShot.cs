using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoxShot : ActiveMorph
{
    [SerializeField] private EdgeProjectileSpawner poxShotSpawner;

    private void OnValidate()
    {
        poxShotSpawner.OnValidate();
    }

    private void OnDrawGizmos()
    {
        poxShotSpawner.OnDrawGizmos(transform);
    }
}
