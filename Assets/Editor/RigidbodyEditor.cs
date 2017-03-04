using UnityEditor;
using UnityEngine;
public class RigidbodyEditor : Editor
{
    void OnSceneGUI()
    {
        if (target is Rigidbody)
        {
            Rigidbody rb = target as Rigidbody;
            Handles.color = Color.magenta;
            Handles.SphereCap(1, rb.transform.TransformPoint(rb.centerOfMass), rb.rotation, rb.transform.localScale.x * .1f);
        }
    }
}