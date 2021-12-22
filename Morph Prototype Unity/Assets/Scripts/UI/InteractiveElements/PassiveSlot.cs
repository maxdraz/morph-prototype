using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSlot : Slot
{
    protected override void Awake()
    {
        base.Awake();
        dropConditions.Add(new IsPassiveMorphDropCondition());
    }

    public override void Equip(DraggableComponent draggableComponent)
    {
        base.Equip(draggableComponent);

        var player = PlayerCreatureCharacter.Instance;
        var morphPrefab = draggableComponent.GetComponent<MorphCollectionData>().MorphPrefab;

        if (morphPrefab && player)
        {
            var morphLoadout = player.PartyManager.ActiveCreature.GetComponent<MorphLoadout>();
            morphLoadout.AddMorphToLoadout(morphPrefab);
        }
    }
}
