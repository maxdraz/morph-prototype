using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class ObjectPoolInfo
{
    public GameObject objToPool;
    public int numToPool;
}

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance;
    public static ObjectPooler Instance
    {
        get
        {
            if (!_instance)
            {
               _instance = new GameObject("ObjectPooler").AddComponent<ObjectPooler>();
            }

            return _instance;
        }
    }
    
    [SerializeField]
    private List<ObjectPoolInfo> objectsToPool;
    private List<GameObject> pooledObjects;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("ObjectPooler :: more than one instance");
            Destroy(this);
        }
        
        pooledObjects ??= new List<GameObject>();
        objectsToPool ??= new List<ObjectPoolInfo>();
        
        PrePoolObjects(objectsToPool);
    }

    public void Recycle(GameObject obj)
    {
        AddToPool(obj);
    }
    
    public GameObject GetOrCreatePooledObject(GameObject obj, bool enableTheObj = true)
    {
        var objToReturn = GetObjectFromPool(obj);
        objToReturn ??= Instantiate(obj, null, true);
        objToReturn.SetActive(enableTheObj);
        
        return objToReturn;
    }
    
    

    private GameObject GetObjectFromPool(GameObject obj)
    {
        if (pooledObjects.Count > 0)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                var currentObj = pooledObjects[i];
                var currentObjName = currentObj.name.Remove(currentObj.name.Length - 7); // gets obj name without "(Clone)"
               // if (currentObj.name.Contains(obj.name))
               if (currentObjName == obj.name)
                {
                    pooledObjects.RemoveAt(i);
                    return currentObj;
                }
            }
        }

        return null;
    }

    private void CreateAndPool(GameObject obj)
    {
        AddToPool(Instantiate(obj));
    }
    
    private void CreateAndPool(GameObject obj, int numToPool)
    {
        // make sure num isnt negative
        numToPool = Mathf.Max(1, numToPool);

        for (int i = 0; i < numToPool; i++)
        {
            CreateAndPool(obj);
        }
    }

    private void AddToPool(GameObject obj)
    {
        pooledObjects.Add(obj);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }

    private void PrePoolObjects(List<ObjectPoolInfo> objsToPool)
    {
        if (objsToPool.Count < 1) return;

        foreach (var objInfo in objsToPool)
        {
            print("adding to pool");
            CreateAndPool(objInfo.objToPool, objInfo.numToPool);
        }
    }
}
