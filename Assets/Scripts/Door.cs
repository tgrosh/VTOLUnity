using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Triggereble {
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnTrigger(Trigger trigger)
    {
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
    }
}
