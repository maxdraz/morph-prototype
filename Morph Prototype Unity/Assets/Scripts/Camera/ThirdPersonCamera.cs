using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 pivot;
    [SerializeField] private bool invertPitch= true;
    [SerializeField] private bool invertYaw = false;
    [SerializeField] private float distance;
    [SerializeField] private float anglesPerSec = 90f;
    [SerializeField] private float maxPitch = 90f;
    [SerializeField] private float cameraFollowSpeed = 5f;
    private Follower follower;
  
    private float yawAngle;
    private float pitchAngle;
    private Quaternion pivotYaw => Quaternion.AngleAxis(yawAngle, Vector3.up);
    private Vector3 rotatedPivot => target.transform.position +  (pivotYaw * pivot);
    private Vector3 cameraPosition => rotatedPivot + (pivotYaw * -Vector3.forward  * distance);

    private void Awake()
    {
        follower = new Follower();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
       // UpdateAnglesBasedOnInput();
       // UpdateCameraAndPivotRotation();
    }

    private void LateUpdate()
    {
        UpdateAnglesBasedOnInput();
        UpdateCameraAndPivotRotation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(rotatedPivot, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraPosition, 0.2f);
    }

    private void UpdateAnglesBasedOnInput()
    {
        yawAngle += Input.GetAxis("Mouse X") * anglesPerSec * Time.deltaTime;

        var pitchInput = Input.GetAxis("Mouse Y");
        pitchInput *= invertPitch ? -1 : 1;
        pitchAngle += pitchInput * anglesPerSec * Time.deltaTime;

        if (pitchAngle >= maxPitch)
            pitchAngle = maxPitch;
        else if (pitchAngle <= -maxPitch)
            pitchAngle = -maxPitch;
        
    }

    private void UpdateCameraAndPivotRotation()
    {
        transform.position = cameraPosition;
       // follower.Follow(cameraPosition, transform, cameraFollowSpeed);
       transform.rotation = Quaternion.LookRotation(rotatedPivot + (pivotYaw * Vector3.forward) - transform.position, Vector3.up);
       transform.RotateAround(rotatedPivot, transform.right, pitchAngle);
    }
}
