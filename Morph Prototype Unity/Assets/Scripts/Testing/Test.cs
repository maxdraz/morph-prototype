using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    private HitboxManager hitboxManager;

    [SerializeField] private SphericalHitbox sphericalHitbox;
    
    private void Awake()
    {
        hitboxManager = GetComponent<HitboxManager>();
    }

    private void Start()
    {
        //sphericalHitbox = hitboxManager.GetHtibox<SphericalHitbox>();
    }
}
