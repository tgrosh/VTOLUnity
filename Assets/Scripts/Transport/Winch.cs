using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Winch : MonoBehaviour {
    public WinchHook hook;
    public GameObject[] links;
    public float winchProximityMax;
    public float winchProximityMin;
    public AudioClip attachAudio;
    public AudioClip detachAudio;

    AudioSource audioSource;
    bool winchActive;
    bool triggerPulled;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!triggerPulled && Gamepad.current.rightTrigger.isPressed)
        {
            triggerPulled = true;

            if (!winchActive)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.down, out hit, winchProximityMax);

                if (hit.collider != null)
                {
                    WinchPoint winchPoint = hit.collider.GetComponent<WinchPoint>();
                    if (winchPoint != null)
                    {
                        if (hit.distance > winchProximityMin)
                        {
                            winchActive = true;
                            Connect(winchPoint);
                        }
                    }
                }
            }
        } else if (!Gamepad.current.rightTrigger.isPressed)
        {
            triggerPulled = false;

            if (winchActive)
            {
                winchActive = false;
                Disconnect();
            }
        }
    }

    public void Connect(WinchPoint winchPoint)
    {
        foreach (GameObject o in links)
        {
            o.SetActive(true);
        }
        hook.gameObject.SetActive(true);
        hook.Connect(winchPoint);
        audioSource.PlayOneShot(attachAudio);
    }

    public void Disconnect()
    {
        hook.Disconnect();
        foreach (GameObject o in links)
        {
            o.SetActive(false);
        }
        hook.gameObject.SetActive(false);
        audioSource.PlayOneShot(detachAudio);
    }
}
