using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : MonoBehaviour {
    public float fuelPercentPerSecond;
    public AudioSource pumpAudio;
    public AudioSource dingAudio;

    Animator animator;
    bool isRefueling;
    Transport refuelingTransport;
    FuelTank refuelingTank;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (refuelingTank != null && refuelingTransport.throttle == 0)
        {
            if (!refuelingTank.isFull)
            {
                isRefueling = true;
                refuelingTank.Fill(fuelPercentPerSecond/100f * refuelingTank.maxFuel * Time.deltaTime);
                if (!pumpAudio.isPlaying)
                {
                    pumpAudio.Play();
                }
            }
            else
            {
                if (isRefueling)
                {
                    pumpAudio.Stop();
                    dingAudio.Play();
                }
                isRefueling = false;
            }
            animator.SetBool("isRefueling", isRefueling);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();
        FuelTank tank = collider.gameObject.GetComponentInParent<FuelTank>();

        if (transport != null && tank != null)
        {
            refuelingTransport = transport;
            refuelingTank = tank;
        }
    }
    
    void OnTriggerExit(Collider collider)
    {
        if (refuelingTank == null || refuelingTransport == null) return;

        FuelTank tank = collider.gameObject.GetComponentInParent<FuelTank>();
        if (tank != null)
        {
            if (isRefueling && !dingAudio.isPlaying)
            {
                dingAudio.Play();
            }
            isRefueling = false;
            refuelingTank = null;
            refuelingTransport = null;
            animator.SetBool("isRefueling", false);
            pumpAudio.Stop();
        }
    }
}
