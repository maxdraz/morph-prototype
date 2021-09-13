using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions
{
    public static Vector3 CameraForwardOnPlane(Vector3 planeNormal)
    {
        return Vector3.ProjectOnPlane(Camera.main.transform.forward, planeNormal).normalized;
    }
}
