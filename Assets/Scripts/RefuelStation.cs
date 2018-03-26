using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : MonoBehaviour {
    public float fuelPerSecond;
    public AudioSource pumpAudio;
    public AudioSource dingAudio;

    Animator animator;
    bool isRefueling;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void OnTriggerStay(Collider collider)
    {
        Transport transport = collider.gameObject.GetComponentInParent<Transport>();
        FuelTank tank = collider.gameObject.GetComponentInParent<FuelTank>();

        if (tank != null && transport.throttle == 0)
        {
            if (!tank.isFull)
            {
                isRefueling = true;
                tank.Fill(fuelPerSecond * Time.deltaTime);
                if (!pumpAudio.isPlaying)
                {
                    pumpAudio.Play();
                }
            } else
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
    
    void OnTriggerExit(Collider collider)
    {
        FuelTank tank = collider.gameObject.GetComponentInParent<FuelTank>();
        if (tank != null)
        {
            if (isRefueling && !dingAudio.isPlaying)
            {
                dingAudio.Play();
            }
            isRefueling = false;
            animator.SetBool("isRefueling", false);
            pumpAudio.Stop();
        }
    }
}
