using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public float triggerWeight;
    
    List<GameObject> currentRoots = new List<GameObject>();
    float currentMass;
    bool triggered;

    void Reset () {
        triggerWeight = 75f;        
	}
        
    void OnCollisionEnter (Collision collision)
    {
        GameObject colliderRoot = collision.collider.transform.root.gameObject;

        if (currentRoots.Contains(colliderRoot))
        {
            return;
        }
        
        currentRoots.Add(colliderRoot);
        currentMass += colliderRoot.GetComponent<Rigidbody>().mass;

        if (!triggered & currentMass > triggerWeight)
        {
            GetComponent<Switch>().On(gameObject, true);
            triggered = true;
        }        
    }

    void OnCollisionExit(Collision collision)
    {
        GameObject colliderRoot = collision.collider.transform.root.gameObject;

        if (currentRoots.Contains(colliderRoot))
        {
            currentRoots.Remove(colliderRoot);
            currentMass -= colliderRoot.GetComponent<Rigidbody>().mass;
            
            if (triggered && currentMass < triggerWeight)
            {
                GetComponent<Switch>().Off(gameObject);
                triggered = false;
            }
        }        
    }
}
