using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {
    public float fuelPerSecond;
    public float thrustForce;
    public float thrustValue;
    public ParticleSystem engine;
    public ParticleSystem trail;
    public AudioSource thrusterAudio;
    public Rigidbody forceBody;
    public FuelTank fuelTank;
    public ParticleSystem[] cutoffParticles;
    public float maxThrust = 1f;
    public bool erratic;
    public float thrusterCutoutDuration = .25f;
    float thrusterCutoutChance = .5f;

    bool cutout;
    float thrusterCutoutCurrent = 0f;
    ParticleSystem.MainModule engineMain;
    ParticleSystem.MainModule trailMain;
    ParticleSystem.EmissionModule trailEmission;  
    float engineSizeMax;
    float engineSpeedMax;
    float trailSizeMax;
    float trailSpeedMax;
    float trailEmissionRateMax;

    // Use this for initialization
    void Start () {
        engineMain = engine.main;
        trailMain = trail.main;
        trailEmission = trail.emission;

        engineSizeMax = engineMain.startSize.constant;
        engineSpeedMax = engineMain.startSpeed.constant;
        trailSizeMax = trailMain.startSize.constant;
        trailSpeedMax = trailMain.startSpeed.constant;
        trailEmissionRateMax = trail.emission.rateOverTime.constant;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (cutout) thrustValue = 0;

        thrustValue *= maxThrust;

        engineMain.startSize = engineSizeMax * thrustValue;
        engineMain.startSpeed = engineSpeedMax * thrustValue;
        trailMain.startSize = trailSizeMax * thrustValue;
        trailMain.startSpeed = trailSpeedMax * thrustValue;
        trailEmission.rateOverTime = trailEmissionRateMax * thrustValue;
        thrusterAudio.pitch = thrustValue;
        thrusterAudio.volume = thrustValue;
    }

    void FixedUpdate()
    {
        thrustValue *= maxThrust;

        if (fuelTank.fuelRemaining <= 0)
        {
            thrustValue = 0;
        }

        if (erratic)
        {
            if (!cutout)
            {
                float rand = UnityEngine.Random.value;
                if (rand < thrusterCutoutChance * Time.deltaTime)
                {
                    //turn off the thruster
                    cutout = true;
                    PlayEngineCutoff();
                }
            }
            else
            {
                thrusterCutoutCurrent += Time.deltaTime;
                if (thrusterCutoutCurrent > thrusterCutoutDuration)
                {
                    cutout = false;
                    thrusterCutoutCurrent = 0f;
                }
            }
        } else
        {
            cutout = false;
            thrusterCutoutCurrent = 0f;
        }
        
        if (!cutout && thrustValue > 0)
        {
            forceBody.AddRelativeForce(Vector3.up * thrustForce * thrustValue, ForceMode.Force);
            fuelTank.Drain(fuelPerSecond * Time.fixedDeltaTime * thrustValue);
        }
    }
    
    public void PlayEngineCutoff()
    {
        foreach (ParticleSystem cutoff in cutoffParticles)
        {
            cutoff.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            cutoff.Play();
        }
    }
}
