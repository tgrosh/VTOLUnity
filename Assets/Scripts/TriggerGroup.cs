using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGroup : Triggerable
{
    public Triggerable[] targets;

    void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0f, 0.580f, 1f, 0.5F);
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.magnitude);

        gizmoColor.a = 1f;
        Gizmos.color = gizmoColor;
        foreach (Triggerable target in targets)
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

    public override void OnTrigger(GameObject source)
    {
        foreach (Triggerable target in targets)
        {
            target.OnTrigger(source);
        }
    }
}
