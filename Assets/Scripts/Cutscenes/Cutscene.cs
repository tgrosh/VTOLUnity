using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    public PlayableDirector timeline;
    public float skipSpeed;

    bool skipping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.yButton.wasPressedThisFrame)
        {
            skipping = true;
        }

        if (skipping)
        {
            if (timeline.time < timeline.duration)
            {
                timeline.time += Time.deltaTime * skipSpeed;
            }
        }
    }
}
