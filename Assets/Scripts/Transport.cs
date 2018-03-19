using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Transport : Explodable {
    public Rigidbody body;
    public GameObject actor;
    public Winch winch;
    public Thruster[] thrusters;
    public CargoHold cargoHold;
    public GameObject spotLight;
    public GameObject[] statusLEDs;
    public GameObject[] landingLights;
    public Scanner scanner;
    public Transform cargoDropPoint;
    public ParticleSystem impactParticles;
    public GameObject impactDecal;
    public TransportDamageModel damageModel;
    public AudioSource impactAudio;
    public float maxIntegrity;
    public float currentIntegrity;
    public float impactDamageThreshold;
    public float maxRotation;
    public float rotationSpeed;
    public float winchProximityMax;
    public float winchProximityMin;
    public bool thrustersEnabled = true;

    bool inShutdown;
    float shutdownDuration = 5f;
    float currentShutdownTime;
    int currentStatus; //0 = green, 1 = red
    bool cargoDropSafe;
    float impactIntegrityFactor = .25f; // percentage of maxIntegrity. .25 = 25% of max integrity is considered a full impact
    float thrusterMaxPercent = 1f;
    bool throttleErratic = false;
    float thrusterOffCurrent = 0f;
    float thrusterOffChance = .75f;
    float thrusterOffDuration = .075f;

    // Use this for initialization
    void Start()
    {
        currentIntegrity = maxIntegrity;
        Explodable e = GetComponent<Explodable>();
        explosionPrefab = e.explosionPrefab;
        explosionForce = e.explosionForce;
        explosionRadius = e.explosionRadius;
    }

    void Update()
    {
        if (exploded) return;
        
        foreach (GameObject led in statusLEDs)
        {
            led.GetComponent<Animator>().SetInteger("status", currentStatus);
        }

        if (inShutdown)
        {
            if (currentShutdownTime <= shutdownDuration)
            {
                currentShutdownTime += Time.deltaTime;
                thrustersEnabled = false;
                spotLight.SetActive(false);
                currentStatus = 1;
            } else
            {
                currentShutdownTime = 0f;
                thrustersEnabled = true;
                currentStatus = 0;
                inShutdown = false;
            }
        } else
        {
            RaycastHit hit;
            Physics.Raycast(winch.transform.position, Vector3.down, out hit, winchProximityMax);
            
            if (hit.collider != null)
            {
                if (cargoHold.Peek() != null)
                {
                    cargoDropSafe = (hit.distance * .75f > cargoHold.Peek().GetBounds().size.y);
                }

                bool landing = hit.collider.GetComponent<Pad>() != null;
                foreach (GameObject light in landingLights)
                {
                    light.SetActive(landing);
                }
            } else
            {
                cargoDropSafe = true;

                foreach (GameObject light in landingLights)
                {
                    light.SetActive(false);
                }
            }
            
            if (Input.GetAxis("RightTrigger") > 0)
            {
                if (hit.collider != null)
                {
                    Cargo cargo = hit.collider.transform.root.GetComponent<Cargo>();
                    if (cargo != null)
                    {
                        StoreCargo(cargo);
                    } else if (!winch.gameObject.activeInHierarchy)
                    {
                        WinchPoint winchPoint = hit.collider.GetComponent<WinchPoint>();
                        if (winchPoint != null)
                        {
                            if (hit.distance > winchProximityMin)
                            {
                                winch.gameObject.SetActive(true);
                                winch.Connect(winchPoint);
                            }
                        }
                    }
                    else
                    {
                        winch.gameObject.SetActive(false);
                        winch.hook.Disconnect();
                    }
                }
            } else if (Input.GetAxis("LeftTrigger") > 0)
            {
                if (cargoDropSafe)
                {
                    cargoHold.Drop(cargoDropPoint);
                }
            }

            if (Input.GetButtonDown("RightBumper") && !scanner.isScanning)
            {
                scanner.Scan();
            }

            if (Input.GetButtonDown("R3"))
            {
                spotLight.SetActive(!spotLight.activeInHierarchy);
            }
        }                
    }

    private void StoreCargo(Cargo cargo)
    {
        if (this.cargoHold.Store(cargo))
        {
            winch.gameObject.SetActive(false);
        }
    }

    public void Shutdown()
    {
        inShutdown = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (exploded) return;

        if (body.velocity.z < -.05)
        {
            Quaternion actorRotation = Quaternion.Lerp(actor.transform.localRotation, Quaternion.Euler(new Vector3(0, 0f, 0)), Time.fixedDeltaTime * rotationSpeed);
            actor.transform.localRotation = actorRotation;
        }
        else if (body.velocity.z > .05)
        {
            Quaternion actorRotation = Quaternion.Lerp(actor.transform.localRotation, Quaternion.Euler(new Vector3(0, 180f, 0)), Time.fixedDeltaTime * rotationSpeed);
            actor.transform.localRotation = actorRotation;
        }

        Quaternion newRotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(new Vector3(maxRotation * Input.GetAxis("Horizontal"), 0, 0)), Time.fixedDeltaTime * rotationSpeed);
        body.MoveRotation(newRotation);
        
        float throttle = Input.GetAxis("Vertical");
        if (!thrustersEnabled)
        {
            throttle = 0;
        }
        if (throttle >= 0)
        {
            foreach (Thruster t in thrusters)
            {
                t.thrustValue = throttle * thrusterMaxPercent;
            }
        }
        
        ProcessThrottleErratic();
    }

    private void ProcessThrottleErratic()
    {
        if (throttleErratic)
        {
            if (thrustersEnabled == true)
            {
                float rand = UnityEngine.Random.value;
                if (rand < thrusterOffChance * Time.deltaTime)
                {
                    //turn off the thruster
                    thrustersEnabled = false;
                }
            }
            else
            {
                thrusterOffCurrent += Time.deltaTime;
                if (thrusterOffCurrent > thrusterOffDuration)
                {
                    thrustersEnabled = true;
                    thrusterOffCurrent = 0f;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (exploded) return;

        float impactForce = collision.impulse.magnitude;
        if (impactForce > impactDamageThreshold)
        {
            currentIntegrity -= impactForce - impactDamageThreshold;

            impactParticles.transform.position = collision.contacts[0].point;
            float impactFactor = impactForce / (maxIntegrity * impactIntegrityFactor);
            impactParticles.emission.SetBurst(0, new ParticleSystem.Burst(0, 40 * impactFactor));
            ParticleSystem.MainModule impactMain = impactParticles.transform.Find("Dust").GetComponent<ParticleSystem>().main;
            impactMain.startSize = 3 * impactFactor;
            impactParticles.Play(true);
            impactAudio.Play();

            Instantiate(impactDecal, collision.contacts[0].point, Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal), null);

            if (currentIntegrity / maxIntegrity < .25f)
            {
                throttleErratic = true;
            }

            if (currentIntegrity / maxIntegrity < .5f)
            {
                damageModel.ShowDamageModel50();
                thrusterMaxPercent = .8f;
            }

            Debug.LogWarning("Integrity Remaining: " + currentIntegrity);

            if (currentIntegrity <= 0)
            {
                Explode();
            }
        }
    }

    void OnParticleCollision(GameObject go)
    {
        float force = go.transform.localScale.y;
        ParticleCollider pCol = go.GetComponent<ParticleCollider>();
        if (pCol != null)
        {
            force *= pCol.forceScale;
        }
        Vector3 direction = transform.position - go.transform.position;        
        body.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
            
    public override void Explode()
    {
        if (!exploded)
        {
            Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.Follow = null;

            Destroy(transform.Find("CenterOfMass").gameObject);
            Destroy(GetComponent<Rigidbody>());
            foreach (Thruster t in thrusters)
            {
                Destroy(t);
            }

            Transform hauler = transform.Find("Hauler");
            foreach (Transform child in hauler)
            {
                child.gameObject.AddComponent<Rigidbody>();
            }

            base.Explode();
        }
    }
}
