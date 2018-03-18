using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBarrier : Triggerable {
    public bool isActive = true;

    Sparker[] sparkers;

    // Use this for initialization
    void Start () {
        sparkers = transform.GetComponentsInChildren<Sparker>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.transform.root.GetComponent<Transport>();

        if (transport != null)
        {
            transport.Explode();
        }
    }

    public override void OnTrigger(Trigger trigger)
    {
        isActive = !isActive;
        foreach (Sparker sparker in sparkers)
        {
            sparker.lightningBolt.SetActive(isActive);
            sparker.trigger.enabled = isActive;
        }
    }
}
