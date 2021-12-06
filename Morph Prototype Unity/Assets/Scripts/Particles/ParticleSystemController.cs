using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField]private float attackDuration;
    private List<ParticleSystem> particleSystems;
    // Start is called before the first frame update
    void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
    }

    private void Start()
    {
        // if (particleSystems != null && particleSystems.Count > 0)
        // {
        //     if (attackDuration > 0)
        //     {
        //         print("scaling");
        //         ScaleParticlesToDuration(attackDuration);
        //     }
        // }
    }

    private void OnEnable()
    {
        // if (particleSystems != null && particleSystems.Count > 0){
        //     
        //     if (attackDuration > 0)
        //     {
        //         print("scaling");
        //         ScaleParticlesToDuration(attackDuration);
        //     }
        // }
    }

    private void OnDisable()
    {
        //attackDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleParticlesToDuration(float targetDuration)
    {
        if (particleSystems == null)
        {
            particleSystems = GetComponentsInParent<ParticleSystem>().ToList();
        }

        if (particleSystems.Count > 0)
        {
            foreach (var ps in particleSystems)
            {
                ScaleParticleSystemSimulationSpeed(ps, targetDuration);
            }
        }
    }

    private void ScaleParticleSystemSimulationSpeed(ParticleSystem ps, float targetDuration)
    {
        var main = ps.main;
        main.simulationSpeed = CalculateSimulationSpeed(main.duration, targetDuration);
    }

    private float CalculateSimulationSpeed(float currentParticleSystemDuration, float targetDuration)
    {
        return currentParticleSystemDuration * (1 / targetDuration);
    }
}
