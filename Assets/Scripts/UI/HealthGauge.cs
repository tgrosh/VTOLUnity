using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGauge : MonoBehaviour
{
    [Range(0, 1)]
    public float value;
    public Image filler;
    public Text label;

    float colorTranistionSpeed = 5f;
    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = filler.color;

        EventManager.StartListening(EventManager.Events.TransportHealthChange, UpdateHealth);
    }

    private void UpdateHealth(GameObject transportGameObject)
    {
        Transport transport = transportGameObject.GetComponent<Transport>();
        value = transport.currentIntegrity / transport.maxIntegrity;
    }

    // Update is called once per frame
    void Update()
    {
        filler.fillAmount = value;
        label.text = (int)(value * 100) + " %";

        if (value < .25f)
        {
            filler.color = Color.Lerp(filler.color, Color.red, Time.deltaTime * colorTranistionSpeed);
        }
        else if (value < .5f)
        {
            filler.color = Color.Lerp(filler.color, Color.yellow, Time.deltaTime * colorTranistionSpeed);
        }
        else
        {
            filler.color = Color.Lerp(filler.color, originalColor, Time.deltaTime * colorTranistionSpeed);
        }
    }
}
