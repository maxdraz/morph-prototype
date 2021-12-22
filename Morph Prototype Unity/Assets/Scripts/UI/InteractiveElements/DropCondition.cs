using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropCondition
{
   public abstract bool Check(DraggableComponent draggableComponent);
}

public class IsLimbWeaponMorphDropCondition : DropCondition
{
   public override bool Check(DraggableComponent draggableComponent)
   {
      var morphCollectionData = draggableComponent.GetComponent<MorphCollectionData>();

      if (!morphCollectionData) return false;

      var morph = morphCollectionData.MorphPrefab;
      if (morph is LimbWeaponMorph)
      {
         return true;
      }

      return false;
   }
}

public class IsHeadWeaponMorphDropCondition : DropCondition
{
   public override bool Check(DraggableComponent draggableComponent)
   {
      var morphCollectionData = draggableComponent.GetComponent<MorphCollectionData>();

      if (!morphCollectionData) return false;

      var morph = morphCollectionData.MorphPrefab;
      if (morph is HeadWeaponMorph)
      {
         return true;
      }

      return false;
   }
}

public class IsTailWeaponMorphDropCondition : DropCondition
{
   public override bool Check(DraggableComponent draggableComponent)
   {
      var morphCollectionData = draggableComponent.GetComponent<MorphCollectionData>();

      if (!morphCollectionData) return false;

      var morph = morphCollectionData.MorphPrefab;
      if (morph is TailWeaponMorph)
      {
         return true;
      }

      return false;
   }
}

public class IsPassiveMorphDropCondition : DropCondition
{
   public override bool Check(DraggableComponent draggableComponent)
   {
      var morphCollectionData = draggableComponent.GetComponent<MorphCollectionData>();

      if (!morphCollectionData) return false;

      var morph = morphCollectionData.MorphPrefab;
      if (morph is PassiveMorph)
      {
         return true;
      }

      return false;
   }
}


