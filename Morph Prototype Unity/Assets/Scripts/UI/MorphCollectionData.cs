using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorphCollectionData : MonoBehaviour
{
    public Morph MorphPrefab;
    public Sprite icon;

    private void OnValidate()
    {
        var image = GetComponent<Image>();
        if (image)
        {
            image.sprite = icon;
        }
    }
}
