using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {
    public float fuelPerSecond;
    public float thrustForce;
    public float thrustValue;
    public ParticleSystem engine;
    public ParticleSystem trail;
    public Rigidbody forceBody;
    public FuelTank fuelTank;

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
        engineMain.startSize = engineSizeMax * thrustValue;
        engineMain.startSpeed = engineSpeedMax * thrustValue;
        trailMain.startSize = trailSizeMax * thrustValue;
        trailMain.startSpeed = trailSpeedMax * thrustValue;
        trailEmission.rateOverTime = trailEmissionRateMax * thrustValue;
    }

    void FixedUpdate()
    {
        if (fuelTank.fuelRemaining <= 0)
        {
            thrustValue = 0;
        }

        if (thrustValue > 0)
        {
            forceBody.AddRelativeForce(Vector3.up * thrustForce * thrustValue, ForceMode.Force);
            fuelTank.Drain(fuelPerSecond * Time.fixedDeltaTime * thrustValue);
        }
    }
}
