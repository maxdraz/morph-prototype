using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_ParticleDurationtest : MonoBehaviour
{
    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        print("duration " + particleSystem.main.duration);
        var main = particleSystem.main;
        main.simulationSpeed = 2f;
        print("duration " + particleSystem.main.duration);
        print("duration / simspeed " + particleSystem.main.duration / particleSystem.main.simulationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
