using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : MonoBehaviour {
    public float fuelPerSecond;

    Animator animator;
    bool isRefueling;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void OnTriggerStay(Collider collider)
    {
        FuelTank tank = collider.gameObject.transform.root.GetComponent<FuelTank>();

        if (tank != null)
        {
            if (!tank.isFull)
            {
                isRefueling = true;
                tank.Fill(fuelPerSecond * Time.deltaTime);
            } else
            {
                isRefueling = false;
            }
            animator.SetBool("isRefueling", isRefueling);
        }
    }
    
    void OnTriggerExit(Collider collider)
    {
        FuelTank tank = collider.gameObject.transform.root.GetComponent<FuelTank>();
        if (tank != null)
        {
            isRefueling = false;
            animator.SetBool("isRefueling", false);
        }
    }
}
