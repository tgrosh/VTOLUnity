using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : Triggereble {
    public Triggereble target;
    public bool transportTriggerOnly = true;

    GameObject lastColliderRoot;
    List<Collider> colliders = new List<Collider>();
    bool triggerIsActive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (!triggerIsActive) return;

        GameObject colliderRoot = collider.transform.root.gameObject;

        if (transportTriggerOnly && colliderRoot.GetComponent<Transport>() == null)
        {
            return;
        }

        colliders.Add(collider);
        if (lastColliderRoot != colliderRoot)
        {
            target.OnTrigger(this);
        }
        lastColliderRoot = colliderRoot;
    }
    
    void OnTriggerExit(Collider collider)
    {
        if (!triggerIsActive) return;

        colliders.Remove(collider);
        if (colliders.Count == 0)
        {
            lastColliderRoot = null;
        }
    }

    public override void OnTrigger(Trigger trigger)
    {
        triggerIsActive = !triggerIsActive;
    }
}
