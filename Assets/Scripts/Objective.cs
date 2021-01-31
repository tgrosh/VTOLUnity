using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Objective : MonoBehaviour
{
    public PlayableDirector timeline;
    public AudioClip clip;
    public bool transportEnterable;
    public bool cargoCollectable;
    public bool cargoDroppable;

    bool played;

    private void OnTriggerEnter(Collider other)
    {
        if (transportEnterable && other.GetComponentInParent<Transport>() != null)
        {
            Play();
        }

        if (cargoDroppable && other.GetComponentInParent<Cargo>() != null)
        {
            Play();
        }
    }

    private void Play()
    {
        if (clip != null && !played)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
            played = true;
        }

        if (timeline != null && !played)
        {
            timeline.Play();
            played = true;
        }
    }

    public void CollectCargo()
    {
        if (cargoCollectable)
        {
            Play();
        }
    }
}
