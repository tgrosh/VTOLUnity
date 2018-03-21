using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : Switchable {
    public bool transportTriggerOnly = true;

    GameObject lastColliderRoot;
    Dictionary<Collider, GameObject> colliderDictionary = new Dictionary<Collider, GameObject>();
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
    }
    
    // Update is called once per frame
    void Update () {
        List<Collider> collidersToRemove = new List<Collider>();
               
		foreach (Collider collider in colliderDictionary.Keys)
        {
            if (!collider.gameObject.activeInHierarchy)
            {
                collidersToRemove.Add(collider);                
            }
        }

        foreach (Collider collider in collidersToRemove)
        {
            colliderDictionary.Remove(collider);
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (!areaActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;

        if (transportTriggerOnly && colliderRoot.GetComponent<Transport>() == null)
        {
            return;
        }
        
        if (lastColliderRoot != colliderRoot) //new root
        {
            GetComponent<Trigger>().OnTrigger(gameObject);
        }
        lastColliderRoot = colliderRoot; //this root is now the last root

        colliderDictionary.Add(collider, colliderRoot);
    }
    
    void OnTriggerExit(Collider collider)
    {
        if (!areaActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;
        colliderDictionary.Remove(collider);

        if (!colliderDictionary.ContainsValue(colliderRoot))
        {
            lastColliderRoot = null;
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
