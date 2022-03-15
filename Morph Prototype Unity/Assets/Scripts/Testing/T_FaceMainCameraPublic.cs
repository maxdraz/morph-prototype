using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class T_FaceMainCameraPublic : MonoBehaviour
{
    public Camera mainCamera;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(-mainCamera.transform.forward);
    }
}
