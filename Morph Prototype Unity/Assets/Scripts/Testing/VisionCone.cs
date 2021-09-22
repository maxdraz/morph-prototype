using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float visionAngle;
    Vector3 position;
    Transform player;
    Vector3 vectorToPlayer;
    public bool playerInSight;

    // Start is called before the first frame update
    void Start()
    {
        playerInSight = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        vectorToPlayer = position - player.position;

        if (Vector3.Angle(-transform.forward, vectorToPlayer) <= visionAngle)
        {

            playerInSight = true;
        }
        else 
        {
            playerInSight = false;
        }
    }
}
