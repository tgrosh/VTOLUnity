using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportDamageModel : MonoBehaviour {
    public ParticleSystem[] damageLight;
    public ParticleSystem[] damageMedium;
    public ParticleSystem[] electrified;
    public ParticleSystem[] shocked;

    Transport transport;
    bool showingDamageLight;
    bool showingDamageMedium;
    bool showingDamageHeavy;
    bool showingElectrified;

    // Use this for initialization
    void Start () {
        transport = GetComponentInParent<Transport>();
	}
    	
	// Update is called once per frame
	void Update () {
        if (transport.currentIntegrity / transport.maxIntegrity < .25f)
        {
            StartDamageModelHeavy();
        }
        else
        {
            StopDamageModelHeavy();
        }

        if (transport.currentIntegrity / transport.maxIntegrity < .4f)
        {            
            StartDamageModelMedium();
        } else
        {            
            StopDamageModelMedium();
        }

        if (transport.currentIntegrity / transport.maxIntegrity < .6f)
        {
            StartDamageModelLight();
        } else
        {
            StopDamageModelLight();
        }
        if (transport.electrifiedDuration > 0)
        {
            StartElectrified();
        } else
        {
            StopElectrified();
        }
    }
    
    public void StartDamageModelLight()
    {
        if (showingDamageLight) return;

        foreach (ParticleSystem sys in damageLight)
        {
            sys.Play();
        }
        showingDamageLight = true;
    }

    public void StartDamageModelMedium()
    {
        if (showingDamageMedium) return;

        foreach (Thruster thruster in transport.thrusters)
        {
            thruster.maxThrust = .8f;
        }

        foreach (ParticleSystem sys in damageMedium)
        {
            sys.Play();
        }
        showingDamageMedium = true;
    }

    private void StartDamageModelHeavy()
    {
        if (showingDamageHeavy) return;

        foreach (Thruster thruster in transport.thrusters)
        {
            thruster.erratic = true;
        }
        showingDamageHeavy = true;
    }

    public void StopDamageModelLight()
    {
        if (!showingDamageLight) return;

        foreach (ParticleSystem sys in damageLight)
        {
            sys.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
        showingDamageLight = false;
    }

    public void StopDamageModelMedium()
    {
        if (!showingDamageMedium) return;

        foreach (Thruster thruster in transport.thrusters)
        {
            thruster.maxThrust = 1f;
        }

        foreach (ParticleSystem sys in damageMedium)
        {
            sys.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
        showingDamageMedium = false;
    }

    private void StopDamageModelHeavy()
    {
        if (!showingDamageHeavy) return;

        foreach (Thruster thruster in transport.thrusters)
        {
            thruster.erratic = false;
        }
        showingDamageHeavy = false;
    }

    private void StartElectrified()
    {
        if (showingElectrified) return;

        foreach (ParticleSystem sys in electrified)
        {
            sys.Play();
        }
        foreach (Thruster thruster in transport.thrusters)
        {
            thruster.erratic = true;
        }
        showingElectrified = true;
    }

    public void StopElectrified()
    {
        if (!showingElectrified) return;

        foreach (ParticleSystem sys in electrified)
        {
            sys.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
        foreach (Thruster thruster in transport.thrusters)
        {
            thruster.erratic = false;
        }
        showingElectrified = false;
    }

    public void Shock()
    {
        foreach (ParticleSystem sys in shocked)
        {
            sys.Play();
        }
        foreach (Thruster thruster in transport.thrusters)
        {
            thruster.PlayEngineCutoff();
        }
    }
}
