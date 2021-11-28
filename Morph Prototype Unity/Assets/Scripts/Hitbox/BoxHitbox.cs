using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxHitbox : Hitbox
{
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        Deactivate();
    }

}
