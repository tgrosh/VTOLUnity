﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();

        if (transport != null)
        {
            transport.Explode();
        }
    }
}
