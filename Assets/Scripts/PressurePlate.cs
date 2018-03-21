using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public float triggerWeight;
    public Triggerable target;
    public float debounceThreashold;

    bool triggered;
    float lastCollisionTime;
    int collisionCount;

    void Reset () {
        triggerWeight = 75f;
        debounceThreashold = 1f;
	}

    void OnCollisionEnter (Collision collision)
    {
        collisionCount++;
        Debug.Log("Collision Detected (" + collisionCount + ") :" + (Time.realtimeSinceStartup - lastCollisionTime));
        if (!triggered && Time.realtimeSinceStartup - lastCollisionTime > debounceThreashold)
        {
            Debug.Log("Not Triggered");
            if (collision.collider.transform.root.GetComponent<Rigidbody>().mass > triggerWeight)
            {
                Debug.Log("Triggering");
                triggered = true;
                target.OnTrigger(null);
            }
        }
        lastCollisionTime = Time.realtimeSinceStartup;
    }
    void OnCollisionExit()
    {
        collisionCount--;
        triggered = collisionCount != 0;
        Debug.Log("Collision Left (" + collisionCount + ")");
    }
}
