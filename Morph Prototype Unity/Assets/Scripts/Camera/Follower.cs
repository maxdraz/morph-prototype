using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower
{
    public void Follow(Vector3 targetPosition, Transform followerTransform, float speed)
    {
        var toTarget = targetPosition - followerTransform.position;
        var theoreticalVelocity = toTarget.normalized * speed * Time.deltaTime;
        
        Vector3 velocity;
        if (toTarget.magnitude <= theoreticalVelocity.magnitude)
        {
            velocity = theoreticalVelocity.normalized * toTarget.magnitude;
        }
        else
        {
            velocity = theoreticalVelocity;
        }

        if(toTarget.magnitude <=0) return;
        followerTransform.position += velocity;
    }
}
