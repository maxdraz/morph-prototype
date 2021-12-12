using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConeProjectileSpawner : ProjectileSpawner
{
    [Header("Spawning")] 
    [Range(1,25)]
    [SerializeField] private int projectilesToSpawn;
    [Range(0,25)]
    [SerializeField] private float radius;
    
    public override void CalculateSpawnDataLocal()
    {
        projectileSpawnData ??= new List<ProjectileSpawnData>();
        if(projectileSpawnData.Count > 0) 
            projectileSpawnData.Clear();
        
        for (int i = 0; i < projectilesToSpawn; i++)
        {
            var spawnPos = (Vector3)Random.insideUnitCircle * radius;
            projectileSpawnData.Add(new ProjectileSpawnData(spawnPos, spawnPos + Vector3.forward));
        }
    }

    public override List<GameObject> Spawn(Transform transform)
    {
        CalculateSpawnDataLocal();
        
        List<GameObject> projectiles = new List<GameObject>();
        foreach (var spawnData in projectileSpawnData)
        {
            var pos = spawnData.SpawnPosition;
            // rotate forward by pitch and yaw
                // based on radius

            var projectile = ObjectPooler.Instance.GetOrCreatePooledObject(projectilePrefab, false);
            projectile.transform.position = pos;
            projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward);
            projectile.SetActive(true);
            projectiles.Add(projectile);
        }

        return projectiles;
    }
}
