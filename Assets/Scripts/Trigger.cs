using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public Triggereble target;
    public bool transportTriggerOnly = true;

    GameObject lastColliderRoot;
    List<Collider> colliders = new List<Collider>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
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
        colliders.Remove(collider);
        if (colliders.Count == 0)
        {
            lastColliderRoot = null;
        }
    }
}
