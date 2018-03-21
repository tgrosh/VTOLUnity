using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public Triggerable target;

	public void OnTrigger(GameObject source)
    {
        target.OnTrigger(source);
    }
}
