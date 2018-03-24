using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableVirtualCam : Switchable {
    Cinemachine.CinemachineVirtualCamera cam;

    public override void Off(GameObject origin)
    {
        cam.enabled = false;
    }

    public override void On(GameObject origin)
    {
        cam.enabled = true;
    }

    // Use this for initialization
    void Start () {
        cam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
	}
	
}
