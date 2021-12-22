using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbWeaponMorphSlot : Slot
{
    protected override void Awake()
    {
        base.Awake();
        
        dropConditions.Add(new IsLimbWeaponMorphDropCondition());
    }

    public override void Equip(DraggableComponent draggableComponent)
    {
        base.Equip(draggableComponent);

        var player = PlayerCreatureCharacter.Instance;
        var loadout = player.PartyManager.ActiveCreature.GetComponent<MorphLoadout>();

        var morphToAdd = draggableComponent.GetComponent<MorphCollectionData>().MorphPrefab;
        
        if(morphToAdd)
            loadout.AddMorphToLoadout(morphToAdd);
    }
    
    protected override void DequipCurrent()
    {
        print("dequip override called");
        var player = PlayerCreatureCharacter.Instance;
        var loadout = player.PartyManager.ActiveCreature.GetComponent<MorphLoadout>();

        loadout.RemoveLimbWeaponMorph();
        
        base.DequipCurrent();
    }
}
