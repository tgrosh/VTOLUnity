using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Transport : Explodable {
    public float maxIntegrity;
    public float currentIntegrity;
    public float impactDamageThreshold;
    public float maxRotation;
    public float rotationSpeed;

    public GameObject actor;
    public GameObject vehicle;
    public Thruster[] thrusters;
    public Gauge healthGauge;
    public GameObject spotLight;
    public GameObject[] statusLEDs;
    public GameObject[] landingLights;
    public GameObject damageCanvas;
    public GameObject damageTextPrefab;
    public float cargoPickupMaxRange;
    public float cargoSensorRadius;
    public LayerMask cargoSensorMask;
    public Transform cargoDropPoint;
    public ParticleSystem impactParticles;
    public GameObject impactDecal;
    public AudioSource impactAudio;
    public GameObject inputController;

    [HideInInspector]
    public bool thrustersEnabled = true;
    [HideInInspector]
    public float throttle;
    [HideInInspector]
    public float electrifiedDuration = 0f;

    Rigidbody body;
    CargoHold cargoHold;
    CenterOfMass centerOfMass;
    Scanner scanner;
    TransportDamageModel damageModel;

    bool inShutdown;
    float shutdownDuration = 5f;
    float currentShutdownTime;
    int currentStatus; //0 = green, 1 = red
    bool cargoDropSafe;
    float impactIntegrityFactor = .25f; // percentage of maxIntegrity. .25 = 25% of max integrity is considered a full impact
    float electrifiedDamageInterval;
    float currentElectrifiedInterval;
    float electrifiedDamageIntervalAmount;
    float recentDamage;
    float recentDamageTimer;

    void Reset()
    {
        maxRotation = 20;
        rotationSpeed = 10;
        impactDamageThreshold = 150;
        cargoPickupMaxRange = .5f;
        cargoSensorRadius = 1f;
    }

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        cargoHold = GetComponent<CargoHold>();
        centerOfMass = GetComponentInChildren<CenterOfMass>();
        centerOfMass.body = body;
        scanner = GetComponentInChildren<Scanner>();
        damageModel = GetComponentInChildren<TransportDamageModel>();
        currentIntegrity = maxIntegrity;

        Explodable e = GetComponent<Explodable>();
        explosionPrefab = e.explosionPrefab;
        explosionForce = e.explosionForce;
        explosionRadius = e.explosionRadius;
    }

    void Update()
    {
        if (exploded || !inputController.activeInHierarchy) return;

        if (currentIntegrity <= 0)
        {
            Explode();
        }

        healthGauge.value = currentIntegrity / maxIntegrity;

        foreach (GameObject led in statusLEDs)
        {
            led.GetComponent<Animator>().SetInteger("status", currentStatus);
        }

        if (electrifiedDuration > 0)
        {
            if (currentElectrifiedInterval > electrifiedDamageInterval)
            {
                //take damage
                currentIntegrity -= electrifiedDamageIntervalAmount;
                damageModel.Shock();
                currentElectrifiedInterval = 0;
            } else
            {
                currentElectrifiedInterval += Time.deltaTime;
                electrifiedDuration -= Time.deltaTime;
            }
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
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(cargoDropPoint.transform.position, cargoSensorRadius, Vector3.down, 30f, cargoSensorMask);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null)
                {
                    if (cargoHold.Peek() != null)
                    {
                        cargoDropSafe = (hit.distance > cargoHold.Peek().GetBounds().size.y);
                    }

                    bool landing = hit.collider.GetComponent<Pad>() != null;
                    foreach (GameObject light in landingLights)
                    {
                        light.SetActive(landing);
                    }

                    if (Gamepad.current.rightTrigger.isPressed && hit.distance < cargoPickupMaxRange)
                    {
                        Cargo cargo = hit.collider.gameObject.GetComponent<Cargo>();
                        if (cargo != null)
                        {
                            StoreCargo(cargo);
                        }
                    }
                }
                else
                {
                    cargoDropSafe = true;

                    foreach (GameObject light in landingLights)
                    {
                        light.SetActive(false);
                    }
                }
            }

            if (Gamepad.current.leftTrigger.isPressed)
            {
                if (cargoDropSafe)
                {
                    cargoHold.Drop(cargoDropPoint);
                }
            }

            if (Gamepad.current.rightShoulder.wasPressedThisFrame)
            {
                scanner.Scan();
            }

            if (Gamepad.current.rightStickButton.wasPressedThisFrame)
            {
                spotLight.SetActive(!spotLight.activeInHierarchy);
            }
        }

        recentDamageTimer += Time.deltaTime;
        if (recentDamage > 0f)
        {
            if (recentDamageTimer > .5f)
            {
                GameObject damageText = Instantiate(damageTextPrefab, damageCanvas.transform);
                damageText.GetComponent<Text>().text = ((int)recentDamage).ToString();
                Destroy(damageText, 1.1f);
                recentDamage = 0f;
                recentDamageTimer = 0f;
            }
        }
    }

    private void StoreCargo(Cargo cargo)
    {
        cargoHold.Store(cargo);
    }

    public void Shutdown()
    {
        inShutdown = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (exploded || !inputController.activeInHierarchy) return;

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

        Quaternion newRotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(new Vector3(maxRotation * Gamepad.current.rightStick.ReadValue().x, 0, 0)), Time.fixedDeltaTime * rotationSpeed);
        body.MoveRotation(newRotation);

        throttle = Gamepad.current.leftStick.ReadValue().y;
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

        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime /1000f ;
        if (impactForce > impactDamageThreshold)
        {
            currentIntegrity -= impactForce;            
            PlayImpact(collision, impactForce);
            recentDamage += impactForce;
        }
    }

    private void PlayImpact(Collision collision, float impactForce)
    {
        impactParticles.transform.position = collision.contacts[0].point;
        float impactFactor = impactForce / (maxIntegrity * impactIntegrityFactor);
        impactParticles.emission.SetBurst(0, new ParticleSystem.Burst(0, 40 * impactFactor));
        ParticleSystem.MainModule impactMain = impactParticles.transform.Find("Dust").GetComponent<ParticleSystem>().main;
        impactMain.startSize = 3 * impactFactor;
        impactParticles.Play(true);
        impactAudio.Play();

        Instantiate(impactDecal, collision.contacts[0].point, Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal), null);
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
            
    public void Electrify(float interval, float damageIntervalAmount, float duration)
    {
        damageModel.Shock();
        electrifiedDamageInterval = interval;
        electrifiedDamageIntervalAmount = damageIntervalAmount;
        electrifiedDuration = duration;
    }

    public override void Explode()
    {
        if (!exploded)
        {
            thrustersEnabled = false;
            Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.Follow = null;

            Destroy(centerOfMass.gameObject);
            Destroy(GetComponent<Rigidbody>());
            foreach (Thruster t in thrusters)
            {
                Destroy(t);
            }

            foreach (Transform child in vehicle.transform)
            {
                if (child.gameObject.layer != 8) //Jockey layer
                {
                    child.gameObject.AddComponent<Rigidbody>();
                }
            }

            base.Explode();
        }
    }
}
