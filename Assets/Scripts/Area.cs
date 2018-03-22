using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : Switchable {
    public bool transportOnly = true;

    List<GameObject> currentRoots = new List<GameObject>();
    bool areaActive = true;
    Color gizmoColor = Color.red;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }

    void Reset()
    {
        gizmoColor = Color.red;
        transportOnly = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!areaActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;

        if (currentRoots.Contains(colliderRoot))
        {
            return;
        }

        if (transportOnly && colliderRoot.GetComponent<Transport>() == null)
        {
            return;
        }

        currentRoots.Add(colliderRoot);
        TriggerEnter();
    }

    void OnTriggerExit(Collider collider)
    {
        if (!areaActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;
        if (currentRoots.Contains(colliderRoot))
        {
            currentRoots.Remove(colliderRoot);
            TriggerExit();
        }
    }

    protected void TriggerEnter()
    {

    }

    protected void TriggerExit()
    {

    }

    public override void On(GameObject origin)
    {
        areaActive = true;
    }

    public override void Off(GameObject origin)
    {
        areaActive = false;
    }
}
