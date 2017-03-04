using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Winchable winchable = collider.gameObject.GetComponent<Winchable>();

        if (winchable != null && winchable.winchPoint.hook != null)
        {
            winchable.winchPoint.hook.Disconnect();
        }
    }
}
