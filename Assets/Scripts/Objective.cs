using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Objective : MonoBehaviour
{
    public PlayableDirector timeline;
    public AudioClip clip;
    public GameObject effectPrefab;
    public bool isComplete;

    bool played;
    GameObject objectiveEffect;

    protected virtual void Start()
    {
        if (effectPrefab) objectiveEffect = Instantiate(effectPrefab, transform);
    }

    protected void OnComplete()
    {
        isComplete = true;
        if (objectiveEffect) objectiveEffect.SetActive(false);
        Play();
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

}
