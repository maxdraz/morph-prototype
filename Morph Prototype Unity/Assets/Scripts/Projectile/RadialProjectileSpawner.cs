using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RadialProjectileSpawner : ProjectileSpawner
{
    [Header("Spawning")]
    [Range(1,20)]
    [SerializeField] private int projectilesToSpawn = 3;
    [Range(0,50)]
    [SerializeField] private float radius = 1;
    [Range(0,360)]
    [SerializeField] private float radialOffset = 0;
    [Range(-70,70)]
    [SerializeField] private float projectileDirectionRotation = 0;
    
    
    public override void CalculateSpawnDataLocal()
    {
        projectileSpawnData ??= new List<ProjectileSpawnData>();
        
        if(projectileSpawnData.Count > 0) 
            projectileSpawnData.Clear();
        
        float angleIncrement = 360f / projectilesToSpawn;
        radialOffset %= 360f;
        for (int i = 0; i < projectilesToSpawn; i++)
        {
            var rotation = Quaternion.AngleAxis((angleIncrement * i) + radialOffset, Vector3.up);

            var spawnPos = (rotation * Vector3.forward) * radius;
            var spawnDir = spawnPos + spawnPos.normalized;
            spawnDir = Quaternion.AngleAxis(projectileDirectionRotation, Vector3.up) * spawnDir;
            projectileSpawnData.Add(new ProjectileSpawnData(spawnPos,(spawnDir - spawnPos).normalized));
        }
    }

    public override void OnValidate()
    {
        base.OnValidate();

        radialOffset = radialOffset > 360f ? 0 : radialOffset;
    }

    public override void OnDrawGizmos(Transform transform)
    {
        if(!drawGizmos) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.TransformPoint(spawnPointOffsetLocal), radius);
        Gizmos.color = Color.red;
        foreach (var positionAndDirection in projectileSpawnData)
        {
            Gizmos.DrawSphere(transform.TransformPoint(positionAndDirection.SpawnPosition), debugSphereRadius);
            Gizmos.DrawLine(transform.TransformPoint(positionAndDirection.SpawnPosition),
                transform.TransformPoint(positionAndDirection.SpawnPosition) +transform.TransformDirection(positionAndDirection.EndPoint));
        }
    }
}
