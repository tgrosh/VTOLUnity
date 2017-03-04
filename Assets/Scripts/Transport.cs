using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour {
    public Rigidbody body;
    public Thruster[] thrusters;
    public Winch winch;

    public float thrustForce;
    public float maxRotation;
    public float rotationSpeed;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {        
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
	void FixedUpdate () {
		if (Input.GetAxis("Vertical") > 0)
        {
            body.AddRelativeForce(Vector3.up * thrustForce * Input.GetAxis("Vertical"), ForceMode.Force);            
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
}
