using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBarrier : Switchable {
    Sparker[] sparkers;

    // Use this for initialization
    void Start () {
        sparkers = transform.GetComponentsInChildren<Sparker>();
	}
	
    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.transform.root.GetComponent<Transport>();

        if (transport != null)
        {
            transport.Explode();
        }
    }
    
    public override void On(GameObject origin)
    {
        foreach (Sparker sparker in sparkers)
        {
            sparker.lightningBolt.SetActive(true);
            sparker.trigger.enabled = true;
        }
    }

    public override void Off(GameObject origin)
    {
        foreach (Sparker sparker in sparkers)
        {
            sparker.lightningBolt.SetActive(false);
            sparker.trigger.enabled = false;
        }
    }
}
