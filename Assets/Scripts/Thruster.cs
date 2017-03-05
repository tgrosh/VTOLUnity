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
    float engineSizeMax;
    float engineSpeedMax;
    float trailSizeMax;
    float trailSpeedMax;

    // Use this for initialization
    void Start () {
        engineMain = engine.main;
        trailMain = trail.main;

        engineSizeMax = engineMain.startSize.constant;
        engineSpeedMax = engineMain.startSpeed.constant;
        trailSizeMax = trailMain.startSize.constant;
        trailSpeedMax = trailMain.startSpeed.constant;
    }
	
	// Update is called once per frame
	void Update ()
    {
        engineMain.startSize = engineSizeMax * thrustValue;
        engineMain.startSpeed = engineSpeedMax * thrustValue;
        trailMain.startSize = trailSizeMax * thrustValue;
        trailMain.startSpeed = trailSpeedMax * thrustValue;
    }

    void FixedUpdate()
    {
        if (fuelTank.fuelRemaining <= 0)
        {
            thrustValue = 0;
        }

        forceBody.AddRelativeForce(Vector3.up * thrustForce * thrustValue, ForceMode.Force);
        fuelTank.Drain(fuelPerSecond * Time.fixedDeltaTime * thrustValue);
    }
}
