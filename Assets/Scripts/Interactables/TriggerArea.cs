﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : Switchable {
    public bool transportOnly = true;

    List<Rigidbody> currentRoots = new List<Rigidbody>();
    bool areaActive = true;

    void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0f, 0.580f, 1f, 0.25F);
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }

    void Reset()
    {
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
            currentRoots.Add(rootRigidBody);
            GetComponent<Trigger>().OnTrigger(gameObject);
        }
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
