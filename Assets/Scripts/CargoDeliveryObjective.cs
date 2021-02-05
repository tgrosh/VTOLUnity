using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoDeliveryObjective : Objective
{
    public Cargo expectedCargo;

    protected override void Start()
    {
        EventManager.StartListening(EventManager.Events.CargoDelivery, OnDelivery);
        base.Start();
    }

    private void OnDelivery(GameObject gameObject)
    {
        if (expectedCargo == null || gameObject == expectedCargo.gameObject)
        {
            OnComplete();
        }
    }
}
