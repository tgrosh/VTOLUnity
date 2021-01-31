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
    
    void OnCollisionEnter(Collision collision)
    {
        Winchable winchable = collision.gameObject.GetComponent<Winchable>();

        if (winchable != null && winchable.winchPoint.hook != null)
        {
            winchable.winchPoint.hook.Disconnect();
        }

        Cargo cargo = collision.gameObject.GetComponent<Cargo>();

        if (cargo != null)
        {
            Destroy(cargo, 1f);
        }
    }
}
