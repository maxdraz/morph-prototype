using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas parentCanvas;
    private Canvas ownerCanvas;
    private RectTransform rectTransform;
    private ScrollRect scrollRect;

    private Vector2 startPosition;

    public Canvas OwnerCanvas => ownerCanvas;
    public RectTransform RectTransform => rectTransform;

    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        ownerCanvas = GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        scrollRect = GetComponentInParent<ScrollRect>();

    }

    public virtual void OnInitializePotentialDrag(PointerEventData eventData)
    { 
        startPosition = rectTransform.anchoredPosition;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        ownerCanvas.overrideSorting = true;
        ownerCanvas.sortingOrder = 2;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

   
    public void OnEndDrag(PointerEventData eventData)
    {
        // raycast to find slot
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        Slot slot = null;
        ScrollRect scrollRect = null;

        foreach (var result in results)
        {
            slot = result.gameObject.GetComponent<Slot>();
            scrollRect = result.gameObject.GetComponent<ScrollRect>();
            
            if(scrollRect) break;
            if (slot) break;
        }

        if (slot)
        {
            if (slot.Accepts(this))
            {
                slot.Equip(this);
                return;
            }
        }

        if (scrollRect)
        {
            RemoveRelevantMorphFromLoadout();
            var content = scrollRect.content;
            rectTransform.SetParent(content);
        }
        
        ReturnToCollection();
    }

    public void ReturnToCollection()
    {
        if (rectTransform.parent != scrollRect.content)
        {
            rectTransform.SetParent(scrollRect.content);
        }
        
        
        rectTransform.anchoredPosition = startPosition;
        ownerCanvas.overrideSorting = false;
        ownerCanvas.sortingOrder = 0;
    }

    private void RemoveRelevantMorphFromLoadout()
    {
        var morph = GetComponent<MorphCollectionData>().MorphPrefab;

        if (morph is LimbWeaponMorph)
        {
            print("should have removed morph");
            PlayerCreatureCharacter.Instance.CurrentCreatureMorphLoadout.RemoveLimbWeaponMorph();
        }
    }
}
