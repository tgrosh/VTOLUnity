﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public AudioClip explosionAudioClip;

    public float duration
    {
        get
        {
            return Mathf.Max(explosionAudioClip.length, GetComponent<ParticleSystem>().main.duration);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Explode()
    {
        Explode(0, 0);
    }

    public void Explode(float force, float radius)
    {
        AudioSource.PlayClipAtPoint(explosionAudioClip, transform.position);

        if (force > 0f)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody body = hit.GetComponent<Rigidbody>();

                if (body != null)
                {
                    body.isKinematic = false;
                    body.AddExplosionForce(force, explosionPos, radius, .5f);
                }
            }
        }
    }
}
