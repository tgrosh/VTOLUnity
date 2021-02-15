using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArea : Switchable {
    public bool transportOnly = true;
    public bool persistent = false;

    List<Rigidbody> currentRoots = new List<Rigidbody>();
    bool areaActive = true;
    
    void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0.901f, 0.678f, 0.898f, 0.25F);
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }

    void Reset()
    {
        persistent = false;
        transportOnly = true;
    }
        
    void OnTriggerEnter(Collider collider)
    {
        if (!areaActive) return;

        Rigidbody rootRigidBody = collider.attachedRigidbody;

        if (rootRigidBody == null)
        {
            return;
        }

        if (transportOnly && rootRigidBody.GetComponent<Transport>() == null)
        {
            return;
        }

        if (!currentRoots.Contains(rootRigidBody))
        {
            GetComponent<Switch>().On(gameObject, persistent);
        }
        currentRoots.Add(rootRigidBody);
    }

    void OnTriggerExit(Collider collider)
    {
        if (!areaActive) return;

        Rigidbody rootRigidBody = collider.attachedRigidbody;

        if (rootRigidBody == null)
        {
            return;
        }

        if (currentRoots.Contains(rootRigidBody))
        {            
            currentRoots.Remove(rootRigidBody); 
        }
        if (!currentRoots.Contains(rootRigidBody))
        {
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
