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
            winch.gameObject.SetActive(!winch.gameObject.activeInHierarchy);
            winch.hook.Disconnect();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetAxis("Vertical") > 0)
        {
            //Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
            body.AddRelativeForce(Vector3.up * thrustForce * Input.GetAxis("Vertical"), ForceMode.Force);            
        }
        Quaternion newRotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(new Vector3(maxRotation * Input.GetAxis("Horizontal"), 0, 0)), Time.fixedDeltaTime * rotationSpeed);
        body.MoveRotation(newRotation);
        //Debug.Log("body.localRotation: " + body.rotation);


        //if (Input.GetAxis("Horizontal") != 0)
        //{
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        //transform.localRotation = Quaternion.Euler(new Vector3(0, 45f * Input.GetAxis("Horizontal"), 0));
        //body.AddRelativeTorque(Vector3.right * rotationForce * Input.GetAxis("Horizontal"), ForceMode.Force);
        //}

        if (Input.GetAxis("Vertical") >= 0)
        {
            foreach (Thruster t in thrusters)
            {
                t.thrustValue = Input.GetAxis("Vertical");
            }
        }
	}    
}
