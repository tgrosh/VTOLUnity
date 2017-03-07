using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour {
    [Range(0,1)]
    public float value;
    public Image filler;
    public Image labelBackground;
    public Text label;

    float colorTranistionSpeed = 2f;
    Color originalColor;
	// Use this for initialization
	void Start () {
        originalColor = filler.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        filler.fillAmount = value * .5f;
        label.text = (int)(value * 100) + " %";

        if (value < .25f) {
            filler.color = labelBackground.color = Color.Lerp(filler.color, Color.red, Time.deltaTime * colorTranistionSpeed);
            label.color = Color.Lerp(label.color, Color.white, Time.deltaTime * colorTranistionSpeed);
        } else if (value < .5f)
        {
            filler.color = labelBackground.color = Color.Lerp(filler.color, Color.yellow, Time.deltaTime * colorTranistionSpeed);
            label.color = Color.Lerp(label.color, Color.black, Time.deltaTime * colorTranistionSpeed);
        } else
        {
            filler.color = Color.Lerp(filler.color, originalColor, Time.deltaTime * colorTranistionSpeed);
            label.color = Color.Lerp(label.color, Color.black, Time.deltaTime * colorTranistionSpeed);
        }
	}
}
