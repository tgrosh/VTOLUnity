using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    public Switchable target;

    bool persistentOn;
    GameObject origin;
    
    void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0.901f, 0.678f, 0.898f, 1F);
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(transform.position, target.transform.position);
    }

    public void On(GameObject origin, bool persistent)
    {
        target.On(origin);
        persistentOn = persistent;
        this.origin = origin;
    }

    public void Off(GameObject origin)
    {
        target.Off(origin);
        persistentOn = false;
    }

    void Update()
    {
        if (persistentOn)
        {
            target.On(origin);
        }
    }
}
