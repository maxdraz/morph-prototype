using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hitbox : MonoBehaviour
{
    protected Collider col;

    public Action<DamageHandler> Hit;

    private void OnTriggerEnter(Collider other)
    {
        var damageHandler = other.gameObject.GetComponent<DamageHandler>();
        if (!damageHandler) return;
        
        Hit?.Invoke(damageHandler);
    }

    public void Activate()
    {
        col.enabled = true;
    }

    public void Deactivate()
    {
        col.enabled = false;
    }
}
