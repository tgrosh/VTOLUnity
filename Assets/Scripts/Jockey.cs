using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jockey : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Rigidbody b in transform.GetComponentsInChildren<Rigidbody>())
        {
            b.isKinematic = true;
        }
        foreach (CapsuleCollider c in transform.GetComponentsInChildren<CapsuleCollider>())
        {
            c.enabled = false;
        }
        foreach (BoxCollider c in transform.GetComponentsInChildren<BoxCollider>())
        {
            c.enabled = false;
        }
        foreach (SphereCollider c in transform.GetComponentsInChildren<SphereCollider>())
        {
            c.enabled = false;
        }
    }
	
    public void Die()
    {
        foreach (Rigidbody b in transform.GetComponentsInChildren<Rigidbody>())
        {
            b.isKinematic = false;
        }
        foreach (CapsuleCollider c in transform.GetComponentsInChildren<CapsuleCollider>())
        {
            c.enabled = true;
        }
        foreach (BoxCollider c in transform.GetComponentsInChildren<BoxCollider>())
        {
            c.enabled = true;
        }
        foreach (SphereCollider c in transform.GetComponentsInChildren<SphereCollider>())
        {
            c.enabled = true;
        }
        GetComponentInChildren<Animator>().enabled = false;
    }
}
