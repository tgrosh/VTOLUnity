using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Triggerable {
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnTrigger(GameObject trigger)
    {
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
    }
}
