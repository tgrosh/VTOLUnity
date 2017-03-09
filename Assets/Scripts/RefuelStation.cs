using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : MonoBehaviour {
    public float fuelPerSecond;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
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
