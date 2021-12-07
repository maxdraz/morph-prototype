using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;

    private Vector2 startPosition;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
       print("initialized drag");
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("drag begun");
    }


    public void OnDrag(PointerEventData eventData)
    {
        print("dragging");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

   
    public void OnEndDrag(PointerEventData eventData)
    {
       // check if dropped on a slot
       rectTransform.anchoredPosition = startPosition;
    }
}
