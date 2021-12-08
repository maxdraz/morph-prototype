using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    protected List<DropCondition> dropConditions = new List<DropCondition>();

    protected RectTransform myRectTransform;
    protected DraggableComponent currentlyEquipped;

    protected virtual void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    public bool Accepts(DraggableComponent draggableComponent)
    {
        return dropConditions.TrueForAll(condition => condition.Check(draggableComponent));
    }

    public virtual void Equip(DraggableComponent draggableComponent)
    {
        // get rid of equip
        DequipCurrent();
        
        draggableComponent.RectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        draggableComponent.RectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        draggableComponent.RectTransform.SetParent(myRectTransform, false);
        draggableComponent.RectTransform.anchoredPosition = Vector2.zero;
        
        draggableComponent.OwnerCanvas.sortingOrder = 1;

        currentlyEquipped = draggableComponent;
    }

    protected virtual void DequipCurrent()
    {
        if (currentlyEquipped == null) return;
        
        currentlyEquipped.ReturnToCollection();
        currentlyEquipped = null;

    }
}
