using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excavator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play("ExcavatorDig", -1, Random.Range(0f, 1f));
	}
	
}
