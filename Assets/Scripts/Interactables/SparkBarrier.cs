using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBarrier : Switchable {
    public float interval;
    public float damagerPerInterval;
    public float duration;

    Sparker[] sparkers;

    // Use this for initialization
    void Start () {
        sparkers = transform.GetComponentsInChildren<Sparker>();
	}
	
    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();

        if (transport != null)
        {
            transport.Electrify(interval, damagerPerInterval, duration);
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
