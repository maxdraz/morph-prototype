using System.Collections;
using System.Collections.Generic;
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
      //  var toCamera = mainCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-mainCamera.transform.forward);
    }
}
