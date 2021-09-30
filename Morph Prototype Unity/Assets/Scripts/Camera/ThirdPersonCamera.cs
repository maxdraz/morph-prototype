using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]  bool drawGizmos;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 pivotOffset;
    [SerializeField] private float cameraDistance;
    [SerializeField] private float cameraMaxDistance;
    [SerializeField] private float cameraDistanceBeforeCollision;
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
    private bool cameraFirstCollision = true;
    [SerializeField]private float spherecastRadius;
    [SerializeField]private float spherecastDistance;
    
    RaycastHit hit;
    
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

        if (!target)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        cameraMaxDistance = cameraDistance;

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

            yawAngle %= 360f;

            pitchAngle = Mathf.Clamp(pitchAngle, pitchMinMax.x, pitchMinMax.y);
        }
        
        //zoom
        Zoom2();
        //ZoomRaycast();

        //yaw around world centre
        if (pivotOffset.x != 0)
        {
            pivotPosition = pivotOffset;
            Quaternion q = Quaternion.AngleAxis(yawAngle, Vector2.up);
            pivotPosition = q * pivotOffset;
            //transform to target pos
            pivotPosition += target.position;

            //get forward vec of pivot
            pivotRight = target.position - pivotPosition;
            pivotRight.y = 0;
            cross = Vector3.Cross(pivotRight, target.transform.up).normalized;
            cross = pivotOffset.x > 0 ? cross : -cross; 

            //pitch
            //transofrm cam to pivot 
            transform.position = pivotPosition;
            transform.LookAt(transform.position - cross);
            transform.RotateAround(pivotPosition, transform.right, pitchAngle);

        }
        else
        {
            //transform.rotation = Quaternion.identity;
            transform.position = target.position;
            var rot = Quaternion.Euler(pitchAngle, yawAngle, 0);
            transform.rotation = rot;
            
        }
        
        TranslateCamera2();

    }

    private void TranslateCamera()
    {
        //sphere cast backwards
       // RaycastHit hit;
        if (Physics.SphereCast(pivotPosition, spherecastRadius, -transform.forward, out hit, cameraDistance + 0.2f))
        {
            if (cameraFirstCollision)
            {
                print("initial dist");
                cameraDistanceBeforeCollision = cameraDistance;
                cameraFirstCollision = false;
            }
            cameraDistance = hit.distance;
        }
        else
        {
            cameraFirstCollision = true;
        }

        cameraDistance = Mathf.Min(cameraDistance, cameraDistanceBeforeCollision);

        transform.position += -transform.forward * cameraDistance;
    }

    private void TranslateCamera2()
    {
        transform.position += -transform.forward * cameraDistance;
    }
    
    private void Zoom()
    {
        var scroll = -Input.GetAxis("Mouse ScrollWheel");
        cameraDistance += scroll *  zoomIncrement;
        cameraDistance = Mathf.Clamp(cameraDistance, zoomMinMax.x, zoomMinMax.y);
        
        if (Physics.SphereCast(pivotPosition, spherecastRadius, -transform.forward, out hit, cameraDistance + 0.2f))
        {
           
            cameraDistance = hit.distance;
        }
        
        cameraDistance = Mathf.Clamp(cameraDistance, zoomMinMax.x, zoomMinMax.y);
    }
    
    private void Zoom2()
    {
        var scroll = -Input.GetAxis("Mouse ScrollWheel");
       

        if (Physics.SphereCast(pivotPosition, spherecastRadius, -transform.forward, out hit, cameraDistance + 0.2f))
        {
            if (cameraFirstCollision)
            {
                cameraDistanceBeforeCollision = cameraDistance;
                cameraFirstCollision = false;
            }
            cameraDistance = Mathf.Min(hit.distance, cameraDistanceBeforeCollision);

            if (scroll < 1) // can only zoom in while colliding with environment
            {
                cameraDistance += scroll *  zoomIncrement;
                if (cameraDistance < hit.distance)
                {
                    return;
                }
            }
        }
        else
        {
            if (!cameraFirstCollision)
            {
                cameraDistance = cameraDistanceBeforeCollision;
               
            }
            cameraFirstCollision = true;
            
            cameraDistance += scroll *  zoomIncrement;
        }
        cameraDistance = Mathf.Clamp(cameraDistance, zoomMinMax.x, zoomMinMax.y);
    }
    
    private void Zoom3()
    {
        var scroll = -Input.GetAxis("Mouse ScrollWheel");
       

        if (Physics.SphereCast(pivotPosition, spherecastRadius, -transform.forward, out hit, cameraDistance + 0.2f))
        {
            if (cameraFirstCollision)
            {
                cameraDistanceBeforeCollision = cameraDistance;
                cameraFirstCollision = false;
            }
            
            cameraMaxDistance = hit.distance;

            if (scroll < 1) // can only zoom in while colliding with environment
            {
                cameraDistance += scroll * zoomIncrement;
            }
        }
        else
        {
            if (!cameraFirstCollision)
            {
                cameraDistance = cameraDistanceBeforeCollision;
               
            }
            cameraFirstCollision = true;
            
            cameraMaxDistance = zoomMinMax.y;
        }
        
        cameraDistance += scroll * zoomIncrement;
        
        cameraDistance = Mathf.Clamp(cameraDistance, zoomMinMax.x, cameraMaxDistance);
    }
    
    private void ZoomRaycast()
    {
        var scroll = -Input.GetAxis("Mouse ScrollWheel");
       

        if (Physics.Raycast(pivotPosition, -transform.forward, out hit, zoomMinMax.y + spherecastDistance))
        {
            if (cameraFirstCollision)
            {
               // cameraDistanceBeforeCollision = cameraDistance;
                cameraFirstCollision = false;
            }
            //cameraDistance = Mathf.Min(hit.distance - spherecastDistance , cameraDistanceBeforeCollision);

            //cameraDistance = hit.distance - spherecastDistance;

            cameraMaxDistance = Mathf.Min(hit.distance, cameraDistanceBeforeCollision);

            if (scroll < 1) // can only zoom in while colliding with environment
            {
                cameraDistance += scroll *  zoomIncrement;
                if (cameraDistance < hit.distance)
                {
                    return;
                }
            }
        }
        else
        {
            if (!cameraFirstCollision)
            {
                cameraDistance = cameraDistanceBeforeCollision;
               
            }
            cameraFirstCollision = true;
            
            cameraDistance += scroll *  zoomIncrement;
        }
        cameraDistance = Mathf.Clamp(cameraDistance, zoomMinMax.x, cameraMaxDistance);
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
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position , spherecastRadius);
        Gizmos.DrawLine(transform.position, transform.position - (transform.forward * spherecastDistance));
        Gizmos.DrawSphere(hit.point , 0.2f);
        
        
    }
}
