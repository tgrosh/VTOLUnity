using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Objective : MonoBehaviour
{
    public PlayableDirector timeline;
    public AudioClip clip;
    public bool isComplete;

    bool played;
    
    protected void OnComplete()
    {
        isComplete = true;
        Play();
    }

    protected void Play()
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

}
