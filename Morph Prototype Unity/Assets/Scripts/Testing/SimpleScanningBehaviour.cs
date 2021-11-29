using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleScanningBehaviour : MonoBehaviour
{
    public List<Vector3> locationsNearby = new List<Vector3>();
    NavMeshAgent agent;
    float wanderingPeriod;
    public float minWanderingPeriod;
    public float maxWanderingPeriod;

    Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderingPeriod = Random.Range(5, maxWanderingPeriod);
        Invoke("Wander", wanderingPeriod);
    }

    void Update()
    {
        if (Input.GetKeyDown("left shift"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine("Investigate", player.transform.position);
        }
    }

    IEnumerator Investigate(Vector3 position) 
    {
        Debug.Log("Investigating");
        CancelInvoke();
        SetDestination(transform.position);
        Vector3 direction = position - transform.position;
        
        //This method of turning is not working
        Quaternion toRotation = Quaternion.LookRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 300f * Time.deltaTime);

        yield return new WaitForSeconds((Random.value * 3) + 2);

        Invoke("Wander", wanderingPeriod);
        yield return null;
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
