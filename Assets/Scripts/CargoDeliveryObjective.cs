using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoDeliveryObjective : Objective
{
    public Cargo expectedCargo;

    private void Start()
    {
        EventManager.StartListening(EventManager.Events.CargoDelivery, OnDelivery);
    }

    private void OnDelivery(GameObject gameObject)
    {
        if (expectedCargo == null || gameObject == expectedCargo.gameObject)
        {
            OnComplete();
        }
    }
}
