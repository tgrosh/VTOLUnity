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
    Material[] actorOrigMaterials;

    void Reset()
    {
        scanDuration = 5f;
        scanDelay = 1.5f;
    }

    void Start()
    {
        actorOrigMaterials = actor.GetComponent<Renderer>().materials;
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
        Material[] materials = actor.materials;
        for (int x = 0; x < materials.Length; x++)
        {
            materials[x] = actorOrigMaterials[x];
        }
        actor.materials = materials;
    }

    void ShowScanningMaterial()
    {
        isShowingScan = true;
        Material[] materials = actor.materials;
        for (int x=0; x<materials.Length; x++)
        {
            materials[x] = scanActiveMaterial;
        }
        actor.materials = materials;
    }
}
