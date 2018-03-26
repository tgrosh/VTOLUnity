using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public float repairPerSecond;
    public AudioSource repairAudio;
    public AudioSource prepareAudio;

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

        if (transport != null && transport.throttle == 0)
        {
            if (transport.currentIntegrity < transport.maxIntegrity)
            {
                if (!isRepairing)
                {
                    prepareAudio.Play();
                }
                isRepairing = true;
                transport.currentIntegrity += repairPerSecond * Time.deltaTime;
                if (transport.currentIntegrity > transport.maxIntegrity)
                {
                    transport.currentIntegrity = transport.maxIntegrity;
                }
                if (!repairAudio.isPlaying)
                {
                    repairAudio.Play();
                }
            }
            else
            {
                if (isRepairing)
                {
                    repairAudio.Stop();
                    prepareAudio.Play();
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
            if (isRepairing)
            {
                prepareAudio.Play();
            }
            isRepairing = false;
            animator.SetBool("isRepairing", false);
            repairAudio.Stop();
        }
    }
}
