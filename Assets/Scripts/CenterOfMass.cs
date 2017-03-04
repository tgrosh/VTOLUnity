using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CenterOfMass : MonoBehaviour {
    public Rigidbody body;
    	
	// Update is called once per frame
	void Update () {
        body.centerOfMass = transform.localPosition;
	}
}
