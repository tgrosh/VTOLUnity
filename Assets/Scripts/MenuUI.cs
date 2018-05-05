using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        MenuCam.gameObject.SetActive(false);
        InputController.SetActive(true);
        Invoke("Hide", 1f);
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
