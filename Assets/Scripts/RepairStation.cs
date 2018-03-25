using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public float repairPerSecond;
    public AudioSource repairAudio;

    Animator animator;
    bool isRepairing;

    void Reset()
    {
        repairPerSecond = 30;
    }

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();

        if (transport != null && transport.currentIntegrity < transport.maxIntegrity && transport.throttle == 0)
        {
            if (transport.currentIntegrity < transport.maxIntegrity)
            {
                isRepairing = true;
                //repair
                //if (!repairAudio.isPlaying)
                //{
                //    repairAudio.Play();
                //}
            }
            else
            {
                if (isRepairing)
                {
                    //repairAudio.Stop();
                }
                isRepairing = false;
            }
            animator.SetBool("isRepairing", isRepairing);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();
        if (transport != null)
        {
            isRepairing = false;
            animator.SetBool("isRepairing", false);
            //repairAudio.Stop();
        }
    }
}
