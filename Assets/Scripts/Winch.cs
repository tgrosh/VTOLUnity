using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winch : MonoBehaviour {
    public WinchHook hook;
    public GameObject[] links;
    public float winchProximityMax;
    public float winchProximityMin;
    public AudioSource attachAudio;
    public AudioSource detachAudio;

    bool winchActive;
    bool triggerPulled;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!triggerPulled && Input.GetAxis("RightTrigger") > 0)
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
        } else if (Input.GetAxis("RightTrigger") <= 0)
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
        attachAudio.Play();
    }

    public void Disconnect()
    {
        hook.Disconnect();
        foreach (GameObject o in links)
        {
            o.SetActive(false);
        }
        hook.gameObject.SetActive(false);
        detachAudio.Play();
    }
}
