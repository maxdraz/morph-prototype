using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoxShot : ActiveMorph
{
    [SerializeField] private EdgeProjectileSpawner poxShotSpawner;

    protected override bool Activate()
    {
        if(base.Activate())
            poxShotSpawner.Spawn(transform);

        return true;
    }

    private void OnValidate()
    {
        poxShotSpawner.OnValidate();
    }

    private void OnDrawGizmos()
    {
        poxShotSpawner.OnDrawGizmos(transform);
    }
}
