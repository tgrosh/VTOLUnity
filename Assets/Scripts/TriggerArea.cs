﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : Switchable {
    public bool transportOnly = true;

    List<GameObject> currentRoots = new List<GameObject>();
    bool areaActive = true;
    Color gizmoColor = new Color(0f, 0.580f, 1f, 0.25F);

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }

    void Reset()
    {
        gizmoColor = new Color(0f, 0.580f, 1f, 0.25F);
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
        GetComponent<Trigger>().OnTrigger(gameObject);
    }
    
    void OnTriggerExit(Collider collider)
    {
        if (!areaActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;
        if (currentRoots.Contains(colliderRoot))
        {
            currentRoots.Remove(colliderRoot);
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
