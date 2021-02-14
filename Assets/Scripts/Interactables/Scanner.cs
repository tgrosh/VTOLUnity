using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Scanner : MonoBehaviour {
    public GameObject scanSphere;
    public float scanSphereSpeed;
    public float scanRadius;

    bool scanning;
    float originalSphereScale;


    private void Update()
    {
        if (scanning)
        {
            scanSphere.SetActive(true);
            scanSphere.transform.localScale *= 1 + Time.deltaTime * scanSphereSpeed;

            if (scanSphere.transform.localScale.x > scanRadius)
            {
                scanning = false;
                scanSphere.SetActive(false);
                scanSphere.transform.localScale = Vector3.one * originalSphereScale;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        originalSphereScale = scanSphere.transform.localScale.x;
        scanSphere.SetActive(false);
    }
    	
    public void Scan()
    {
        scanning = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Scannable scannable = other.gameObject.GetComponent<Scannable>();
        if (scannable != null)
        {
            scannable.ShowScan();
        }
    }

}
