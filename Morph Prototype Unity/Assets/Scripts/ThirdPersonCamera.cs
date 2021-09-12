using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]  bool drawGizmos;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 pivotOffset;
    [SerializeField] private float cameraDistance;
    private Vector3 pivotPosition;
    private float pitchAngle;
    private float yawAngle;
    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;
    [SerializeField] private float pitchSensitivity;
    [SerializeField] private float yawSensitivity;
    [SerializeField] private Vector2 pitchMinMax;
    private Vector3 cross;
    private Vector3 pivotRight;
    private Vector2 input;
    [SerializeField] private Vector2 zoomMinMax;
    [SerializeField] private float zoomIncrement;
    
    private void Reset()
    {
        pitchSensitivity = 1f;
        yawSensitivity = 1f;
        
        pitchMinMax = new Vector2(-60, 60);
        zoomMinMax = new Vector2(1, 5);

        zoomIncrement = 0.5f;

        cameraDistance = 5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        pitchAngle = 0;
        yawAngle = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCameraPosition();
    }
    
    void UpdateCameraPosition()
    {
        var input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        if (Application.isFocused)
        {
            if (invertX)
            {
                input.y *= -1;
            }

            if (invertY)
            {
                input.x *= -1;
            }
            yawAngle += input.x * yawSensitivity * Time.deltaTime;
            pitchAngle += -input.y * pitchSensitivity * Time.deltaTime;

            pitchAngle = Mathf.Clamp(pitchAngle, pitchMinMax.x, pitchMinMax.y);
        }
        
        //zoom
        Zoom();

        //yaw around world centre
        pivotPosition = pivotOffset;
        Quaternion q = Quaternion.AngleAxis(yawAngle,Vector2.up);
        pivotPosition = q * pivotOffset;
        //transform to target pos
        pivotPosition += target.position;
        
        //get forward vec of pivot
        pivotRight = target.position - pivotPosition;
        pivotRight.y = 0;
        cross = Vector3.Cross(pivotRight, target.transform.up).normalized;
        
        //pitch
        //transofrm cam to pivot 
        transform.position = pivotPosition;
        transform.LookAt(transform.position - cross);
        transform.RotateAround(pivotPosition, transform.right, pitchAngle);
        transform.position += -transform.forward * cameraDistance;
        
    }

    private void Zoom()
    {
        var scroll = -Input.GetAxis("Mouse ScrollWheel");
        cameraDistance += scroll *  zoomIncrement;
        cameraDistance = Mathf.Clamp(cameraDistance, zoomMinMax.x, zoomMinMax.y);
    }
    
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pivotPosition, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pivotPosition, pivotPosition + cross.normalized);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(pivotPosition, target.position);
    }
}
