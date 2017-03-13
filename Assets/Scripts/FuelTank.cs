using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour {
    public float fuelRemaining;
    public float maxFuel;
    public FuelGauge fuelGauge;

    // Use this for initialization
    void Start()
    {
        fuelRemaining = maxFuel;
        SetFuelGauge();
    }
    
    void Update()
    {

    }

    public void Drain(float drainAmount)
    {
        fuelRemaining -= drainAmount;
        if (fuelRemaining < 0)
        {
            fuelRemaining = 0;
        }
        SetFuelGauge();
    }

    public void Fill(float fillAmount)
    {
        fuelRemaining += fillAmount;
        if (fuelRemaining > maxFuel)
        {
            fuelRemaining = maxFuel;
        }
        SetFuelGauge();
    }

    public bool isFull {
        get
        {
            return fuelRemaining >= maxFuel;
        }
    }
    
    private void SetFuelGauge()
    {
        if (fuelGauge != null && maxFuel > 0)
        {
            fuelGauge.value = fuelRemaining / maxFuel;
        }
    }
}
