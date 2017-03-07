using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour {
    public float fuelRemaining;
    public float maxFuel;
    public FuelGauge fuelGauge;

    // Use this for initialization
    void Start () {
        fuelRemaining = maxFuel;
        fuelGauge.value = fuelRemaining / maxFuel;
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
        fuelGauge.value = fuelRemaining / maxFuel;
    }

    public void Fill(float fillAmount)
    {
        fuelRemaining += fillAmount;
        if (fuelRemaining > maxFuel)
        {
            fuelRemaining = maxFuel;
        }
        fuelGauge.value = fuelRemaining / maxFuel;
    }
}
