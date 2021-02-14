using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGroup : Switchable {
    public Switchable[] targets;
    
    void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0.901f, 0.678f, 0.898f, 0.5F);
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.magnitude);

        gizmoColor.a = 1f;
        Gizmos.color = gizmoColor;
        foreach (Switchable target in targets)
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

    public override void Off(GameObject origin)
    {
        foreach (Switchable target in targets)
        {
            target.Off(origin);
        }
    }

    public override void On(GameObject origin)
    {
        foreach (Switchable target in targets)
        {
            target.On(origin);
        }
    }
    
}
