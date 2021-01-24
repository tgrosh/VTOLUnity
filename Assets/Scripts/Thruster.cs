using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {
    public float fuelPerSecond;
    public float thrustForce;
    public float thrustValue;
    public ParticleSystem engine;
    public ParticleSystem trail;
    public Light engineLight;
    public float engineLightPower;
    public AudioSource thrusterAudio;
    public Rigidbody forceBody;
    public FuelTank fuelTank;
    public ParticleSystem[] cutoffParticles;
    public float maxThrust = 1f;
    public bool erratic;
    public float thrusterCutoutDuration = .25f;
    float thrusterCutoutChance = .5f;

    public bool autoHover;
    public float autoHoverThrust = .001f;
    public float currentAutoHoverThrust = 0f;
    public float hoverHeight = 0f;
    public float targetVelocity = 0f;

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
        
    }

    void FixedUpdate()
    {
        if (erratic || cutout)
        {
            if (!cutout)
            {
                float rand = UnityEngine.Random.value;
                if (rand < thrusterCutoutChance * Time.deltaTime)
                {
                    //turn off the thruster
                    PlayEngineCutoff();
                }
            }
            else
            {
                thrusterCutoutCurrent += Time.deltaTime;
                if (thrusterCutoutCurrent > thrusterCutoutDuration)
                {
                    StopEngineCutoff();
                }
            }
        } else if (!erratic)
        {
            StopEngineCutoff();
        }

        if (autoHover)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                // 0..1 percentage of desired height remaining. 0 = at height
                float hoverHeightPercent = Mathf.Abs((hoverHeight - hit.distance) / hoverHeight);

                //desired velocity with respect to how far we are from desired height. 
                // farther from desired height means more velocity desired
                float currentTargetVelocity = Mathf.Lerp(0, targetVelocity, hoverHeightPercent);

                // 0..1 percentage of desired velocity remaining. 0 = at desired velocty
                float velocityPercent = Mathf.Abs((currentTargetVelocity - forceBody.velocity.y) / currentTargetVelocity);

                if (hit.distance <= hoverHeight) //need to go up
                {
                    if (forceBody.velocity.y < currentTargetVelocity) //at or below target velocity
                    {
                        //need more height, but not at target velocity, so add more thrust
                        currentAutoHoverThrust += Mathf.SmoothStep(0, autoHoverThrust, velocityPercent);
                    } else //rising faster than target velocity
                    {
                        currentAutoHoverThrust -= autoHoverThrust * (1-hoverHeightPercent);
                    }
                } else //need to go down
                {
                    if (forceBody.velocity.y < -currentTargetVelocity) //falling too fast
                    {
                        //need more height, but not at target velocity, so add more thrust
                        currentAutoHoverThrust += Mathf.SmoothStep(0, autoHoverThrust, velocityPercent);
                    } else //not falling fast enough
                    {
                        currentAutoHoverThrust -= autoHoverThrust * (velocityPercent);
                    }
                }
            }
            thrustValue = currentAutoHoverThrust;
        } else
        {
            currentAutoHoverThrust = 0f;
        }

        if (cutout || fuelTank.fuelRemaining <= 0)
        {
            thrustValue = 0;
        }

        thrustValue *= maxThrust;

        engineMain.startSize = engineSizeMax * thrustValue;
        engineMain.startSpeed = engineSpeedMax * thrustValue;
        trailMain.startSize = trailSizeMax * thrustValue;
        trailMain.startSpeed = trailSpeedMax * thrustValue;
        trailEmission.rateOverTime = trailEmissionRateMax * thrustValue;
        thrusterAudio.pitch = thrustValue;
        thrusterAudio.volume = thrustValue;

        engineLight.intensity = thrustValue * engineLightPower;

        if (!cutout && thrustValue > 0)
        {
            forceBody.AddRelativeForce(Vector3.up * thrustForce * thrustValue, ForceMode.Force);
            fuelTank.Drain(fuelPerSecond * Time.fixedDeltaTime * thrustValue);
        }
    }
    
    public void PlayEngineCutoff()
    {
        cutout = true;
        foreach (ParticleSystem cutoff in cutoffParticles)
        {
            cutoff.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            cutoff.Play();
        }
    }
    
    public void StopEngineCutoff()
    {
        cutout = false;
        thrusterCutoutCurrent = 0f;
    }
}
