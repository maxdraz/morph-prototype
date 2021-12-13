using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileSpawnData
{
    public Vector3 SpawnPosition;
    public Vector3 EndPoint;
    public Vector3 Direction;

    public ProjectileSpawnData(Vector3 spawnPosition, Vector3 endPoint)
    {
        SpawnPosition = spawnPosition;
        EndPoint = endPoint;
    }
}

public class ProjectileSpawnerComponent : MonoBehaviour
{
   
    private enum ProjectileSpawnMode
    {
        Edge,
        Radial
    }

    
    [SerializeField] private bool loop;
    [SerializeField] private GameObject projectilePrefab;
    [Range(0.05f, 4)]
    [SerializeField] private float projectileRadius = 0.25f;
    [SerializeField] private ProjectileSpawnMode spawnMode;
    [SerializeField] private List<ProjectileSpawnData> projectileSpawnData;
    //[SerializeField] private List<GameObject> projectiles;

    [SerializeField] private bool drawGizmos;
    [Header("Edge Spawn Settings")]
    [SerializeField] private Vector3 edgeSpawnPointOffset;
    [SerializeField] private float spawnLineWidth = 3f;
    [Range(1,20)]
    [SerializeField] private int projectilesToSpawn = 3;
    [Range(0,180)]
    [SerializeField] private float angle;

    [Range(1, 10)]
    [SerializeField] private float debugLineLength = 5;
    

    private Vector3 spawnLineMinPos
        => projectileSpawnPoint + (transform.right * (-spawnLineWidth / 2));
    private Vector3 spawnLineMaxPos
        => spawnLineMinPos + transform.right * spawnLineWidth;
    private Vector3 projectileSpawnPoint => transform.position + transform.TransformDirection(edgeSpawnPointOffset);

    [Header("Radial Spawn Settings")] 
    [SerializeField] private float radius = 1;
    [SerializeField] private Vector3 radialSpawnPointOffset;
    [Range(1,20)]
    [SerializeField] private int projectilesToSpawnRadially = 3;
    [Range(-180,180)]
    [SerializeField] private float bulletDirectionRotationRadial = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoopSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LoopSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (loop)
            {
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
                    //Instantiate(projectilePrefab, pos, Quaternion.LookRotation(dir));

                }
            }
        }
    }
    

    private void OnDrawGizmos()
    {
        if(!drawGizmos) return;

        if (spawnMode == ProjectileSpawnMode.Edge)
        {
            DrawEdgeModeGizmos();
        }
        else
        {
            DrawRadialModeGizmos();
        }
    }

    private void DrawEdgeModeGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(spawnLineMinPos, spawnLineMaxPos);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(projectileSpawnPoint, 0.2f);

        foreach (var projectileSpawnInfo in projectileSpawnData)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                transform.TransformPoint(projectileSpawnInfo.SpawnPosition),
                transform.TransformPoint(projectileSpawnInfo.SpawnPosition)
                + transform.TransformDirection(projectileSpawnInfo.EndPoint));

            Gizmos.DrawSphere(
                transform.TransformPoint(projectileSpawnInfo.SpawnPosition),
                projectileRadius);
        }
    }
    
    private void DrawRadialModeGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.TransformPoint(radialSpawnPointOffset), radius);
        Gizmos.color = Color.red;
        foreach (var positionAndDirection in projectileSpawnData)
        {
            Gizmos.DrawSphere(transform.TransformPoint(positionAndDirection.SpawnPosition), projectileRadius);
            Gizmos.DrawLine(transform.TransformPoint(positionAndDirection.SpawnPosition),
                transform.TransformPoint(positionAndDirection.SpawnPosition) +transform.TransformDirection(positionAndDirection.EndPoint));
        }
    }

    private void OnValidate()
    {
        projectilesToSpawn = projectilesToSpawn <= 0 ? 1 : projectilesToSpawn;
        //recalculate 
        RecalculateProjectileSpawnInfo();
    }

    private void RecalculateProjectileSpawnInfo()
    {
        if(projectileSpawnData != null)
            projectileSpawnData.Clear();

        if (spawnMode == ProjectileSpawnMode.Edge)
        {
            for (int i = 0; i < projectilesToSpawn; i++)
            {
                var offset = Vector3.right * (spawnLineWidth / (projectilesToSpawn - 1) * i);
                var minPos = edgeSpawnPointOffset + (-Vector3.right * (spawnLineWidth / 2));
                var spawnPoint = minPos + offset;
                var endPoint = Quaternion.AngleAxis(-angle / 2 + (angle / (projectilesToSpawn - 1) * i), Vector3.up) *
                               Vector3.forward;
                endPoint += spawnPoint;

                projectileSpawnData.Add(new ProjectileSpawnData(spawnPoint, (endPoint - spawnPoint).normalized));
            }
            
            return;
        }
        
        
        //radial
        float angleIncrement = 360f / projectilesToSpawnRadially;
        for (int i = 0; i < projectilesToSpawnRadially; i++)
        {
            var rotation = Quaternion.AngleAxis(angleIncrement * i, Vector3.up);

            var spawnPos = (rotation * Vector3.forward) * radius;
            var spawnDir = spawnPos + spawnPos.normalized;
            spawnDir = Quaternion.AngleAxis(bulletDirectionRotationRadial, Vector3.up) * spawnDir;
            projectileSpawnData.Add(new ProjectileSpawnData(spawnPos,(spawnDir - spawnPos).normalized));
        }
        
    }
}
