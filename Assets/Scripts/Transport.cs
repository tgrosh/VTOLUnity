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
                bool landing = hit.collider.GetComponent<Pad>() != null;
                foreach (GameObject light in landingLights)
                {
                    light.SetActive(landing);
                }
            } else
            {
                foreach (GameObject light in landingLights)
                {
                    light.SetActive(false);
                }
            }
            
            if (Input.GetButtonDown("RightBumper"))
            {
                if (!winch.gameObject.activeInHierarchy)
                {
                    if (hit.collider != null)
                    {
                        WinchPoint winchPoint = hit.collider.GetComponent<WinchPoint>();
                        if (winchPoint != null)
                        {
                            if (hit.distance > winchProximityMin)
                            {
                                winch.gameObject.SetActive(true);
                                winch.Connect(winchPoint);

                                Cargo cargo = hit.collider.transform.root.GetComponent<Cargo>();
                                if (cargo != null)
                                {
                                    winch.Retract();
                                }
                            }
                        } else
                        {
                            //Debug.Log("Looking for Cargo");
                            //Cargo cargo = hit.collider.transform.root.GetComponent<Cargo>();
                            //if (cargo != null)
                            //{
                            //    //ingest the cargo
                            //    if (this.cargoHold.Store(cargo))
                            //    {
                            //        Debug.Log("Stored Cargo");
                            //    }
                            //}
                        }
                    }
                }
                else
                {
                    winch.gameObject.SetActive(false);
                    winch.hook.Disconnect();
                }
            }

            if (Input.GetButtonDown("R3"))
            {
                spotLight.SetActive(!spotLight.activeInHierarchy);
            }
        }                
    }

    private IEnumerator StoreCargo(Cargo cargo, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (this.cargoHold.Store(cargo))
        {
            winch.gameObject.SetActive(false);
            Debug.Log("Stored Cargo");
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
                t.thrustValue = throttle;
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
            Camera.main.GetComponent<SmoothFollow>().target = null;

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
