using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArea : Switchable {
    public bool transportOnly = true;
    public bool persistent = false;

    List<Rigidbody> currentRoots = new List<Rigidbody>();
    bool areaActive = true;
    Color gizmoColor = new Color(0.901f, 0.678f, 0.898f, 0.25F);
    
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }

    void Reset()
    {
        gizmoColor = new Color(0.901f, 0.678f, 0.898f, 0.25F);
        persistent = false;
        transportOnly = true;
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (!areaActive) return;

        Rigidbody rootRigidBody = collider.gameObject.GetComponentInParent<Rigidbody>();

        if (rootRigidBody == null)
        {
            return;
        }

        if (currentRoots.Contains(rootRigidBody))
        {
            return;
        }

        if (transportOnly && rootRigidBody.GetComponent<Transport>() == null)
        {
            return;
        }

        currentRoots.Add(rootRigidBody);
        GetComponent<Switch>().On(gameObject, persistent);
    }

    void OnTriggerExit(Collider collider)
    {
        if (!areaActive) return;

        Rigidbody rootRigidBody = collider.gameObject.GetComponentInParent<Rigidbody>();

        if (rootRigidBody == null)
        {
            return;
        }

        if (currentRoots.Contains(rootRigidBody))
        {
            currentRoots.Remove(rootRigidBody);
            GetComponent<Switch>().Off(gameObject);
        }        
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
