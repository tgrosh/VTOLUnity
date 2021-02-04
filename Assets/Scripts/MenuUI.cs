using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class MenuUI : MonoBehaviour {
    public GameObject startTimeline;

    void Update()
    {
        //look for Start Button
        if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame || 
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnStartPressed();
        }
    }

    public void OnStartPressed()
    {
        startTimeline.GetComponent<PlayableDirector>().Play();
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
