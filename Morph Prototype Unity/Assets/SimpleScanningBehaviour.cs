using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleScanningBehaviour : MonoBehaviour
{
    public List<Vector3> locationsNearby = new List<Vector3>();
    NavMeshAgent agent;
    float wanderingPeriod;
    float maxWanderingPeriod = 10f;

    Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderingPeriod = Random.Range(5, maxWanderingPeriod);
        Invoke("Wander", wanderingPeriod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wander()
    {
        center = this.gameObject.transform.position;
        locationsNearby.Clear();
        Debug.Log("Wandering");
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * 20f;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                locationsNearby.Add(hit.position);

            }
        }

        wanderingPeriod = Random.Range(1, maxWanderingPeriod);
        Invoke("Wander", wanderingPeriod);
        int chosenLocationToMoveTo = Random.Range(0, locationsNearby.Count);
        SetDestination(locationsNearby[chosenLocationToMoveTo]);
    }

     Vector3 SetDestination(Vector3 newDestination)
    {
        
        agent.destination = newDestination;


        return agent.destination;
    }
}
