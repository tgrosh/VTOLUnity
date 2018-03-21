using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : Triggerable {
    public Triggerable target;
    public bool transportTriggerOnly = true;

    GameObject lastColliderRoot;
    Dictionary<Collider, GameObject> colliderDictionary = new Dictionary<Collider, GameObject>();
    bool triggerIsActive = true;
    	
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
        if (!triggerIsActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;

        if (transportTriggerOnly && colliderRoot.GetComponent<Transport>() == null)
        {
            return;
        }
        
        if (lastColliderRoot != colliderRoot) //new root
        {
        }
        lastColliderRoot = colliderRoot; //this root is now the last root

        colliderDictionary.Add(collider, colliderRoot);
    }
    
    void OnTriggerExit(Collider collider)
    {
        if (!triggerIsActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;
        colliderDictionary.Remove(collider);

        if (!colliderDictionary.ContainsValue(colliderRoot))
        {
            lastColliderRoot = null;
        }
    }

    {
        triggerIsActive = !triggerIsActive;
    }
}
