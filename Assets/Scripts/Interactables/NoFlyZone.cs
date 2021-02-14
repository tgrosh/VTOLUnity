using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFlyZone : MonoBehaviour {
    void OnDrawGizmos()
    {
        Color gizmoColor = new Color(1f, 0.5f, 0f, 0.25F);
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }

    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();
        if (transport != null)
        {
            transport.thrustersEnabled = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();
        if (transport != null)
        {
            transport.thrustersEnabled = true;
        }
    }
}
