using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public float repairPercentPerSecond;
    public AudioSource repairAudio;
    public AudioSource prepareAudio;

    Animator animator;
    bool isRepairing;
    Transport repairingTransport;

    void Reset()
    {
        repairPercentPerSecond = 10;
    }

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (repairingTransport != null && repairingTransport.throttle == 0)
        {
            if (repairingTransport.currentIntegrity < repairingTransport.maxIntegrity)
            {
                if (!isRepairing)
                {
                    prepareAudio.Play();
                }
                isRepairing = true;
                float repairAmountPerSecond = (repairPercentPerSecond / 100f * repairingTransport.maxIntegrity);
                Debug.Log(repairAmountPerSecond);
                repairingTransport.currentIntegrity += repairAmountPerSecond * Time.fixedDeltaTime;
                if (repairingTransport.currentIntegrity > repairingTransport.maxIntegrity)
                {
                    repairingTransport.currentIntegrity = repairingTransport.maxIntegrity;
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

    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();
        if (transport != null)
        {
            repairingTransport = transport;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (repairingTransport == null) return;

        Transport transport = collider.gameObject.GetComponentInParent<Transport>();
        if (transport != null)
        {
            if (isRepairing)
            {
                prepareAudio.Play();
            }
            repairingTransport = null;
            isRepairing = false;
            animator.SetBool("isRepairing", false);
            repairAudio.Stop();
        }
    }
}
