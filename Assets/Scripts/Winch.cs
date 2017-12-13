using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winch : MonoBehaviour {
    public WinchHook hook;

	// Use this for initialization
	void Start () {
        AsyncRetract retract = new AsyncRetract(this.Retract);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Connect(WinchPoint winchPoint)
    {
        hook.Connect(winchPoint);
    }

    public delegate void AsyncRetract();

    public void Retract()
    {

    }
}
