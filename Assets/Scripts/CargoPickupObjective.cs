using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoPickupObjective : Objective
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(EventManager.Events.CargoPickup, OnCargoPickup);
    }

    private void OnCargoPickup(GameObject gameObject)
    {
        if (this.gameObject == gameObject)
        {
            OnComplete();
        }
    }
}
