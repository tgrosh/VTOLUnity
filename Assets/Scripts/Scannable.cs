using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour {
    public Material scanActiveMaterial;
    public Renderer actor;
    public float scanDuration;
    public float scanDelay;

    bool isScanning;
    bool isShowingScan;
    float currentScanTime = 0f;
    Material actorOrigMaterial;

    void Reset()
    {
        scanDuration = 5f;
        scanDelay = 1.5f;
    }

    void Update()
    {
        if (isScanning)
        {
            currentScanTime += Time.deltaTime;
            if (currentScanTime > scanDuration)
            {
                EndScan();
                currentScanTime = 0f;
            } else if (currentScanTime > scanDelay && !isShowingScan)
            {
                ShowScanningMaterial();
            }
        }
    }
    
    public void ShowScan()
    {
        isScanning = true;        
    }

    void EndScan()
    {
        isScanning = false;
        isShowingScan = false;
        actor.material = actorOrigMaterial;
        actorOrigMaterial = null;
    }

    void ShowScanningMaterial()
    {
        isShowingScan = true;
        actorOrigMaterial = actor.material;
        actor.material = scanActiveMaterial;
    }
}
