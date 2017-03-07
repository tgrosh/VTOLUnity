using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour {
    [Range(0,1)]
    public float value;
    public Image filler;
    public Text label;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        filler.fillAmount = value * .5f;
        label.text = (int)(value * 100) + " %";
	}
}
