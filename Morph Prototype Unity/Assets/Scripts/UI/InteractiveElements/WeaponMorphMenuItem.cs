using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMorphMenuItem : MonoBehaviour
{
    [SerializeField] private WeaponMorph weaponMorphPrefab;
    [SerializeField] private Sprite icon;
    private Image imageComponent;

    // Start is called before the first frame update
    void Awake()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.sprite = icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        if (icon != null)
        {
            if (!imageComponent) imageComponent = GetComponent<Image>();
            
            imageComponent.sprite = icon;
        }
    }
}
