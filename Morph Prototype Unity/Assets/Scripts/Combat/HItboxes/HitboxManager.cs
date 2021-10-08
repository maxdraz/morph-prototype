using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    [SerializeField] private List<Hitbox> hitboxes;

    private void Awake()
    {
        hitboxes = GetComponentsInChildren<Hitbox>().ToList();
    }
    
   // public GetHtibox<SphericalHitbox>();
}
