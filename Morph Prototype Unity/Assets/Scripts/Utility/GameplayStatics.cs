using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayStatics
{
   private static GameObject SpawnParticleSystem(GameObject particleSystemPrefab)
   {
      return ObjectPooler.Instance.GetOrCreatePooledObject(particleSystemPrefab);
   }
   private static GameObject SpawnParticleSystem(GameObject particleSystemPrefab, Vector3 position)
   {
      var particlesObj = ObjectPooler.Instance.GetOrCreatePooledObject(particleSystemPrefab);
      particlesObj.transform.position = position;
      return particlesObj;
   }
   private static GameObject SpawnParticleSystem(GameObject particleSystemPrefab, Vector3 position, Quaternion rotation)
   {
      var particlesObj = SpawnParticleSystem(particleSystemPrefab, position);
      particlesObj.transform.rotation = rotation;
      return particlesObj;
   }
   
   // public static GameObject SpawnParticleSystem(GameObject particleSystemPrefab, Transform parent)
   // {
   //    var particleObj = SpawnparticleSystem(particleSystemPrefab);
   //    particleObj.transform.parent = parent;
   // }

   public static GameObject SpawnParticleSystem(GameObject particleSystemPrefab, Transform parent, Vector3 position, 
      Quaternion rotation)
   {
      var particlesObj = SpawnParticleSystem(particleSystemPrefab, position, rotation);
      particlesObj.transform.parent = parent;
      return particlesObj;
   }

   public static GameObject SpawnParticleSystemOnClosestColliderBounds(GameObject particleSystemPrefab, Vector3 lookAtTargetPos, 
      Collider collider)
   {
      var impactPoint = collider.ClosestPointOnBounds(lookAtTargetPos);
      var toTargetNormalized = (lookAtTargetPos - impactPoint).normalized;
      var lookRotation = Quaternion.LookRotation(toTargetNormalized);
      var particlesObj = SpawnParticleSystem(particleSystemPrefab, impactPoint, lookRotation);

      return particlesObj;
   }
}
