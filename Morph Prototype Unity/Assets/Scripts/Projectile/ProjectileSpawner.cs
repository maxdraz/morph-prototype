using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ProjectileSpawner
{
    [Header("General")] 
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Vector3 spawnPointOffsetLocal = Vector3.forward;
    
    [Header("Debug")]
    [SerializeField] protected bool drawGizmos = true;
    [Range(0.1f, 4)]
    [SerializeField] protected float debugSphereRadius = 0.1f;
    
    protected List<ProjectileSpawnData> projectileSpawnData;

    public abstract void CalculateSpawnDataLocal();

    public virtual void OnValidate()
    {
        CalculateSpawnDataLocal();
    }

    public virtual void OnDrawGizmos(Transform transform) {}

    public virtual List<GameObject> Spawn(Transform transform)
    {
        if(!projectilePrefab || projectileSpawnData.Count <= 0) return null;

        List<GameObject> projectiles = new List<GameObject>();

        foreach (var positionAndDirection in projectileSpawnData)
        {

            var pos = transform.TransformPoint(positionAndDirection.SpawnPosition);
            var endPos = transform.TransformPoint(positionAndDirection.SpawnPosition)
                         + transform.TransformDirection(positionAndDirection.EndPoint);

            var dir = (endPos - pos).normalized;

            var projectile = ObjectPooler.Instance.GetOrCreatePooledObject(projectilePrefab, false);
            projectile.transform.position = pos;
            projectile.transform.rotation = Quaternion.LookRotation(dir);
            projectile.SetActive(true);
            projectiles.Add(projectile);
        }

        return projectiles;
    }
}
