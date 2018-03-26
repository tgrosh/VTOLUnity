using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public float triggerWeight;
    
    List<Rigidbody> currentRoots = new List<Rigidbody>();
    float currentMass;
    bool triggered;

    void Reset () {
        triggerWeight = 75f;        
	}
        
    void OnCollisionEnter (Collision collision)
    {
        Rigidbody rootRigidBody = collision.collider.gameObject.GetComponentInParent<Rigidbody>();

        if (rootRigidBody == null)
        {
            return;
        }

        if (currentRoots.Contains(rootRigidBody))
        {
            return;
        }
        
        currentRoots.Add(rootRigidBody);
        currentMass += rootRigidBody.mass;

        if (!triggered & currentMass > triggerWeight)
        {
            GetComponent<Switch>().On(gameObject, true);
            triggered = true;
        }        
    }

    void OnCollisionExit(Collision collision)
    {
        Rigidbody rootRigidBody = collision.collider.gameObject.GetComponentInParent<Rigidbody>();

        if (rootRigidBody == null)
        {
            return;
        }

        if (currentRoots.Contains(rootRigidBody))
        {
            currentRoots.Remove(rootRigidBody);
            currentMass -= rootRigidBody.mass;
            
            if (triggered && currentMass < triggerWeight)
            {
                GetComponent<Switch>().Off(gameObject);
                triggered = false;
            }
        }        
    }
}
