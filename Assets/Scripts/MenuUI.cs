using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class MenuUI : MonoBehaviour {
    public GameObject startTimeline;

    void Start()
    {
        
    }

    void Update()
    {
        //look for Start Button
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            OnStartPressed();
        }
    }

    public void OnStartPressed()
    {
        //temporary code
        startTimeline.GetComponent<PlayableDirector>().Play();
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
