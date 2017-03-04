using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinchHook : MonoBehaviour {
    Joint joint = null;
    WinchPoint winchPoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Connect(WinchPoint winchPoint)
    {
        winchPoint.hook = this;
        transform.position = winchPoint.transform.position;
        joint = CreateHingeJoint(winchPoint.winchable.gameObject, GetComponent<Rigidbody>(), new Vector3(0, .5f, 0));
    }

    public void Disconnect()
    {
        if (winchPoint != null)
        {
            winchPoint.hook = null;
            Destroy(joint);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        winchPoint = collider.gameObject.GetComponent<WinchPoint>();

        if (winchPoint != null && joint == null)
        {
            //joint = CreateHingeJoint(hook.gameObject, container.GetComponent<Rigidbody>(), new Vector3(0, -1.05f, 0));
            //joint = CreateFixedJoint(hook.gameObject, container.GetComponent<Rigidbody>(), new Vector3(0, -1.05f, 0));

            Connect(winchPoint);
        }
    }

    Joint CreateHingeJoint(GameObject connectedFrom, Rigidbody connectedTo, Vector3 anchorPoint)
    {
        HingeJoint joint = connectedFrom.AddComponent<HingeJoint>();
        JointLimits limits = joint.limits;
        limits.min = 0;
        limits.max = 0;
        joint.limits = limits;
        joint.useLimits = true;
        joint.enablePreprocessing = false;
        joint.anchor = anchorPoint;
        JointSpring spring = joint.spring;
        spring.spring = 100;
        spring.damper = 10;
        joint.spring = spring;
        joint.useSpring = true;
        joint.connectedBody = connectedTo;

        return joint;
    }
}
