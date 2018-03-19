using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportDamageModel : MonoBehaviour {
    public GameObject DamageModel50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowDamageModel50()
    {
        DamageModel50.SetActive(true);
    }
}
