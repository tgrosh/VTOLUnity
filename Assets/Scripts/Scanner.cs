using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {
    public float scannerRadius;
    [HideInInspector]
    public bool isScanning;
    ParticleSystem particles;

    void Reset()
    {
        scannerRadius = 3f;
    }

	// Use this for initialization
	void Start () {
        particles = GetComponentInChildren<ParticleSystem>();
	}

    void Update()
    {
        isScanning = particles.isPlaying;
    }
	
    public void Scan()
    {
        if (!isScanning)
        {
            particles.Play();
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, scannerRadius);
        
        foreach (Collider coll in hitColliders)
        {
            Scannable scannable = coll.gameObject.GetComponentInParent<Scannable>();
            if (scannable != null)
            {
                scannable.ShowScan();
            }
        }
    }

    void EndScanning()
    {
        particles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }
}
