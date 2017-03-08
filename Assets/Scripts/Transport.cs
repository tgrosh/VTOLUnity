using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Transport : Explodable {
    public Rigidbody body;
    public GameObject actor;
    public Winch winch;
    public Thruster[] thrusters;
    public float maxIntegrity;
    public float currentIntegrity;
    public float impactDamageThreshold;
    public float maxRotation;
    public float rotationSpeed;
    public float winchProximityMax;
    public float winchProximityMin;
        
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
         
        if (Input.GetButtonDown("RightBumper"))
        {
            if (!winch.gameObject.activeInHierarchy)
            {
                RaycastHit hit;
                Physics.Raycast(winch.transform.position, Vector3.down, out hit, winchProximityMax);
                if (hit.collider != null)
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
            } else
            {
                winch.gameObject.SetActive(false);
                winch.hook.Disconnect();
            }
        }
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


        if (Input.GetAxis("Vertical") >= 0)
        {
            foreach (Thruster t in thrusters)
            {
                t.thrustValue = Input.GetAxis("Vertical");
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
        
    public override void Explode()
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
