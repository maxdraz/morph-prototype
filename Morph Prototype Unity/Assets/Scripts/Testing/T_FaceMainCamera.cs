using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class T_FaceMainCamera : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(-mainCamera.transform.forward);
    }
}
