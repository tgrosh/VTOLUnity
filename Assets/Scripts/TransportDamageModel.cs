using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportDamageModel : MonoBehaviour {
    public GameObject DamageModel50;
    public ParticleSystem damageSparks;
    public ParticleSystem[] cutoffParticles;

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

    public void ShowDamageSparks()
    {
        damageSparks.Play();
    }

    public void EngineCutoff()
    {
        foreach (ParticleSystem cutoff in cutoffParticles)
        {
            cutoff.Play();
        }
    }
}
