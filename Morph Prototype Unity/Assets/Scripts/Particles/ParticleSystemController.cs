using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    private List<ParticleSystem> particleSystems;
 
    void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
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
