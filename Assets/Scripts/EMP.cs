using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : Triggereble {
    public ParticleSystem pulser;

    public override void OnTrigger(Trigger trigger)
    {
        Debug.Log(pulser.isPlaying);
        if (!pulser.isPlaying)
        {
            pulser.Play(true);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
