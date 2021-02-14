using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Switchable {
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    public override void On(GameObject origin)
    {
        anim.SetBool("isOpen", true);
    }

    public override void Off(GameObject origin)
    {
        anim.SetBool("isOpen", false);
    }
}
