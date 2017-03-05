using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour {
    public float fuelRemaining;
    public float maxFuel;

    // Use this for initialization
    void Start () {
        fuelRemaining = maxFuel;
	}

    void Update()
    {
        Debug.Log("Fuel Remaining: " + fuelRemaining);
    }
	
    public void Drain(float drainAmount)
    {
        fuelRemaining -= drainAmount;
        if (fuelRemaining < 0)
        {
            fuelRemaining = 0;
        }
    }

    public void Fill(float fillAmount)
    {
        fuelRemaining += fillAmount;
        if (fuelRemaining > maxFuel)
        {
            fuelRemaining = maxFuel;
        }
    }
}
