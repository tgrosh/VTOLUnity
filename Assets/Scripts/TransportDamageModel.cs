using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportDamageModel : MonoBehaviour {
    public ParticleSystem[] damageLight;
    public ParticleSystem[] damageMedium;
    
    Transport transport;

    // Use this for initialization
    void Start () {
        transport = GetComponentInParent<Transport>();
	}
    	
	// Update is called once per frame
	void Update () {
        
    }
    
    public void ProcessDamage()
    {
        if (transport.currentIntegrity / transport.maxIntegrity < .25f)
        {
            foreach (Thruster thruster in transport.thrusters)
            {
                thruster.erratic = true;
            }
        }

        if (transport.currentIntegrity / transport.maxIntegrity < .4f)
        {
            foreach (Thruster thruster in transport.thrusters)
            {
                thruster.maxThrust = .8f;
            }
            ShowDamageModelMedium();
        }

        if (transport.currentIntegrity / transport.maxIntegrity < .6f)
        {
            ShowDamageModelLight();
        }
    }

    public void ShowDamageModelLight()
    {
        foreach (ParticleSystem sys in damageLight)
        {
            sys.Play();
        }
    }

    public void ShowDamageModelMedium()
    {
        foreach (ParticleSystem sys in damageMedium)
        {
            sys.Play();
        }
    }
}
