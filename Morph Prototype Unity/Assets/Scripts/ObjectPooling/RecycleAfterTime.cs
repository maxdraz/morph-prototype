using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleAfterTime : MonoBehaviour
{
    private ParticleSystem particleSystem;
    [SerializeField] private bool useParticleSystemDuration = true;
    [SerializeField] private float duration = 1;
    private float actualDuration;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (useParticleSystemDuration)
        {
            var particleSystems = GetComponentsInChildren<ParticleSystem>();
            if (particleSystems.Length > 0)
            {
                float longestDuration = 0;
                foreach (var particle in particleSystems)
                {
                    if (particle.main.duration > longestDuration)
                    {
                        longestDuration = particle.main.duration;
                    }
                }

                actualDuration = longestDuration;
                return;
            }
        }
        
        actualDuration = duration;
    }

    private void OnEnable()
    {
        StartCoroutine(RecycleAfterTimeCoroutine(actualDuration));
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
}
