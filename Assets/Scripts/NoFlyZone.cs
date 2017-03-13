using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFlyZone : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.transform.root.GetComponent<Transport>();
        if (transport != null)
        {
            transport.thrustersEnabled = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Transport transport = collider.transform.root.GetComponent<Transport>();
        if (transport != null)
        {
            transport.thrustersEnabled = true;
        }
    }
}
