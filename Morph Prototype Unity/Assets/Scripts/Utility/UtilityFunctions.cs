using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class UtilityFunctions
{
    public static Vector3 CameraForwardOnPlane(Vector3 planeNormal)
    {
        return Vector3.ProjectOnPlane(Camera.main.transform.forward, planeNormal).normalized;
    }
    
    public static Vector3 TransformForwardOnPlane(Transform trans,Vector3 planeNormal)
    {
        return Vector3.ProjectOnPlane(trans.forward, planeNormal).normalized;
    }
    
    public static T CopyComponent<T>(this GameObject destination, T original) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        System.Reflection.FieldInfo[] fields = type.GetFields(flags);
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
    
    // public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    // {
    //     System.Type type = original.GetType();
    //     var dst = destination.GetComponent(type) as T;
    //     if (!dst) dst = destination.AddComponent(type) as T;
    //     BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
    //     var fields = type.GetFields(flags);
    //     foreach (var field in fields)
    //     {
    //         if (field.IsStatic) continue;
    //         field.SetValue(dst, field.GetValue(original));
    //     }
    //     var props = type.GetProperties(flags);
    //     foreach (var prop in props)
    //     {
    //         if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
    //         prop.SetValue(dst, prop.GetValue(original, null), null);
    //     }
    //     return dst as T;
    // }
}
