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

    public T GetHtibox<T>() where T : Hitbox
    {
        foreach (var hitbox in hitboxes)
        {
            if (hitbox is T)
            {
                return (T)hitbox;
            }
        }

        return null;
    }
}
