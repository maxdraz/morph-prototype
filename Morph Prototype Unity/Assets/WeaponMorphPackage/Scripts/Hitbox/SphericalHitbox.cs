using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SphericalHitbox : Hitbox
{
   private void Awake()
   {
      col = GetComponent<SphereCollider>();
      col.isTrigger = true;
      Deactivate();
   }
}
