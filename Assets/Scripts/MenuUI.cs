using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MenuUI : MonoBehaviour {
    public Cinemachine.CinemachineVirtualCamera MenuCam;
    public GameObject InputController;

    void Start()
    {
        InputController.SetActive(false);
    }

    void Update()
    {
        //look for Start Button
        if (Input.GetButtonDown("Start"))
        {
            OnStartPressed();
        }
    }

    public void OnStartPressed()
    {
        //temporary code
        GameObject.Find("IntroTimeline").GetComponent<PlayableDirector>().Play();
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
