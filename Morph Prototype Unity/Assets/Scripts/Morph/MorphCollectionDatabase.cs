using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphCollectionDatabase : MonoBehaviour
{
    public static MorphCollectionDatabase Instance;
    
    [SerializeField] private List<MorphCollectionData> morphCollection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
