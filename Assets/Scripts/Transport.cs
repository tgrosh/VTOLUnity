using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Transport : Explodable {
    public Rigidbody body;
    public Winch winch;
    public Thruster[] thrusters;
    public float maxIntegrity;
    public float currentIntegrity;
    public float impactDamageThreshold;
    public float maxRotation;
    public float rotationSpeed;
    
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
                Physics.Raycast(winch.transform.position, Vector3.down, out hit, 1f);
                if (hit.collider != null)
                {
                    WinchPoint winchPoint = hit.collider.GetComponent<WinchPoint>();
                    if (winchPoint != null)
                    {
                        //Debug.Log("Distance to Winch Point: " + hit.distance);
                        if (hit.distance > .33f)
                        {
                            winch.gameObject.SetActive(true);
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

        Transform hauler = transform.Find("Hauler");
        foreach (Transform child in hauler)
        {
            if (child.name == "Body")
            {
                Destroy(child.gameObject);
            }
            else
            {
                child.gameObject.AddComponent<Rigidbody>();
            }
        }

        base.Explode();
    }
}
