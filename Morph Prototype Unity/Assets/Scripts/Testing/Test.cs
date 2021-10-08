using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Test : MonoBehaviour
{
   private SpurStab stab;
   private SpurHeavySlash slash;
   
   private void Start()
   {
      stab = new SpurStab(1);
      slash = new SpurHeavySlash(1);
      
      
   }

   void TestAttack(Attack attack)
   {
      if (attack != null)
      {
         
      }

      if (attack is LightAttack)
      {
         print(attack.name + " is light");
      } else if (attack is HeavyAttack)
      {
         print(attack.name + " is heavy");
      }
   }
}
