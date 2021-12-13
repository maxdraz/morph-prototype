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
    [Range(0, 180)] [SerializeField] private float arcAngle;

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
            spawnData.SpawnPosition += spawnPointOffsetLocal;
            spawnData.Direction = CalculateOutwardDirection(spawnData);
            
            //var dir = transform.TransformDirection(spawnData.Direction);
            //var pos = transform.TransformPoint(spawnData.SpawnPosition);
            
            var dir = Camera.main.transform.TransformDirection(spawnData.Direction);
         
            var pos =  Camera.main.transform.TransformPoint(spawnData.SpawnPosition);
            pos += transform.position - Camera.main.transform.position;

            var projectile = ObjectPooler.Instance.GetOrCreatePooledObject(projectilePrefab, false);
            projectile.transform.position = pos;
            projectile.transform.rotation = Quaternion.LookRotation(dir);
            projectile.SetActive(true);
            projectiles.Add(projectile);
        }

        return projectiles;
    }

    private Vector3 CalculateOutwardDirection(ProjectileSpawnData spawnData)
    { 
        var xPosPercentage = spawnData.SpawnPosition.x / (radius * 2);
       var yPosPercentage = -spawnData.SpawnPosition.y / (radius * 2);

       var yawAngle = arcAngle * xPosPercentage;
       var pitchAngle = arcAngle * yPosPercentage;
       
       var rot = Quaternion.AngleAxis(yawAngle, Vector3.up) * Vector3.forward;
       rot = Quaternion.AngleAxis(pitchAngle, Vector3.right) * rot;

       return rot;
    }
}
