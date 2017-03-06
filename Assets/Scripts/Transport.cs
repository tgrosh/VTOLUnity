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

    bool facingRight;
    
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
                        //Debug.Log("Distance to Winch Point: " + hit.distance);
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

        float bodyAngleY = 0;
        if (body.velocity.z < -.05 && facingRight)
        {
            //actor.transform.localEulerAngles = Vector3.zero;
            bodyAngleY = 0;
            Debug.LogError("RESETTING to " + bodyAngleY);
        }
        else if (body.velocity.z > .05 && !facingRight)
        {
            //actor.transform.localEulerAngles = new Vector3(0, 180f, 0);
            bodyAngleY = -180f;
            Debug.LogWarning("ROTATING to " + bodyAngleY);
        }
        
        Vector3 bodyAngle = new Vector3(maxRotation * Input.GetAxis("Horizontal"), bodyAngleY, 0);

        if (facingRight)
        {
            bodyAngle.x *= -1f;
        }
        Debug.LogWarning("bodyAngle " + bodyAngle);
        Quaternion newRotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(bodyAngle), Time.fixedDeltaTime * rotationSpeed);
        body.MoveRotation(newRotation);
        //transform.rotation = newRotation;
        Debug.LogWarning("bodyRotation " + newRotation.eulerAngles);

        if (bodyAngle.y > 175f)
        {
            facingRight = true;
        } else if (bodyAngle.y < 5f)
        {
            facingRight = false;
        }

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
