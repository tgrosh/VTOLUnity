using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : MonoBehaviour {
    public float fuelPerSecond;
        
    void OnTriggerStay(Collider collider)
    {
        FuelTank tank = collider.gameObject.transform.root.GetComponent<FuelTank>();

        if (tank != null)
        {
            if (!tank.isFull)
            {
                tank.Fill(fuelPerSecond * Time.deltaTime);
            }
        }
    }
}
