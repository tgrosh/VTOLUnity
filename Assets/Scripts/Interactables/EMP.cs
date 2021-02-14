using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : Triggerable {
    public ParticleSystem pulser;
    public float radius = 5f;

    bool triggered;
    bool pulsing;

    public override void OnTrigger(GameObject trigger)
    {
        if (!triggered)
        {
            triggered = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //new trigger
        if (triggered && !pulsing)
        {
            pulsing = true;
            pulser.Play(true);
        }

        //triggered and done pulsing
        if (triggered && pulsing && !pulser.isPlaying)
        {
            //explode
            pulsing = false;
            triggered = false;

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Transport transport = hit.gameObject.GetComponentInParent<Transport>();

                if (transport != null)
                {
                    transport.Shutdown();
                }
            }
        }        
    }
    
}
