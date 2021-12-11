using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EdgeProjectileSpawner : ProjectileSpawner
{
    [Header("Spawning")] 
    [Range(1,25)]
    [SerializeField] private int projectilesToSpawn = 1;
    [SerializeField] private float edgeWidth = 1f;
    [Range(0,180)]
    [SerializeField] private float arcAngle = 1f;
    
    
    public override void CalculateSpawnDataLocal()
    {
        projectileSpawnData ??= new List<ProjectileSpawnData>();
        
        if(projectileSpawnData.Count > 0) 
            projectileSpawnData.Clear();
        
        
        for (int i = 0; i < projectilesToSpawn; i++)
        {
            var offset = Vector3.right * (edgeWidth / (projectilesToSpawn - 1) * i);
            var minPos = spawnPointOffsetLocal + (-Vector3.right * (edgeWidth / 2));
            var spawnPoint = minPos + offset;
            var endPoint = Quaternion.AngleAxis(-arcAngle / 2 + (arcAngle / (projectilesToSpawn - 1) * i), Vector3.up) *
                           Vector3.forward;

            if (projectilesToSpawn == 1)
            {
                spawnPoint = spawnPointOffsetLocal;
                endPoint = Quaternion.AngleAxis(arcAngle - 90, Vector3.up) * Vector3.forward;
            }

            endPoint += spawnPoint;

            projectileSpawnData.Add(new ProjectileSpawnData(spawnPoint, (endPoint - spawnPoint).normalized));
        }
    }

    public override void OnDrawGizmos(Transform transform)
    {
        if (!drawGizmos) return;
        
        var right = transform.right;
        var spawnPointOffsetWorld = transform.TransformPoint(spawnPointOffsetLocal);
        Vector3 spawnLineMinPos = spawnPointOffsetWorld + (right * (-edgeWidth / 2));
        Vector3 spawnLineMaxPos = spawnLineMinPos + right * edgeWidth;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(spawnLineMinPos, spawnLineMaxPos);
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawSphere(spawnPointOffsetWorld, 0.2f);

        foreach (var projectileSpawnInfo in projectileSpawnData)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                transform.TransformPoint(projectileSpawnInfo.SpawnPosition),
                transform.TransformPoint(projectileSpawnInfo.SpawnPosition)
                + transform.TransformDirection(projectileSpawnInfo.EndPoint));

            Gizmos.DrawSphere(
                transform.TransformPoint(projectileSpawnInfo.SpawnPosition),
                debugSphereRadius);
        }
    }
}
