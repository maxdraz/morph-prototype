using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecycleAfterTime : MonoBehaviour
{
    [SerializeField] private bool useParticleSystemLifetime = true;
    [SerializeField] private float duration = 1;

    private List<ParticleSystem> particleSystems;

    // Start is called before the first frame update
    void Awake()
    {
       
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        
        if (useParticleSystemLifetime && particleSystems != null)
        {
            StartCoroutine(RecycleIfAllParticlesDeadCoroutine(1));
            return;
        }
        
        StartCoroutine(RecycleAfterTimeCoroutine(duration));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator RecycleAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        ObjectPooler.Instance.Recycle(gameObject);
    }

    private IEnumerator RecycleIfAllParticlesDeadCoroutine(float timeInBetweenChecks)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInBetweenChecks);

            bool shouldRecycle = true;
            foreach (var particleSystem in particleSystems)
            {
                if (particleSystem.IsAlive())
                {
                    shouldRecycle = false;
                }
            }

            if (shouldRecycle)
            {
                ObjectPooler.Instance.Recycle(gameObject);
            }
        }
        
        
       
    }
}
