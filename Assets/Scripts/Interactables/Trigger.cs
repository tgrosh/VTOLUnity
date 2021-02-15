using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public Triggerable target;

    void OnDrawGizmos()
    {
        if (target)
        {
            Color gizmoColor = new Color(0f, 0.580f, 1f, 1F);
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

    public void OnTrigger(GameObject source)
    {
        target.OnTrigger(source);
    }
}
