using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
public class InterfaceController : MonoBehaviour
{
    public CameraController cameraControl;
    public Transform[] panels;
    Transform currentPanel;

    void Start()
    {
        currentPanel = panels[0];
    }

    void Update()
    {
        if (cameraControl.currentView == cameraControl.views[0]
        && currentPanel != panels[0])
        {
            currentPanel.gameObject.SetActive(false);
            currentPanel = panels[0];
            currentPanel.gameObject.SetActive(true);
        }
        if (cameraControl.currentView == cameraControl.views[1]
        && currentPanel != panels[1])
        {
            currentPanel.gameObject.SetActive(false);
            currentPanel = panels[1];
            currentPanel.gameObject.SetActive(true);
        }
        if (cameraControl.currentView == cameraControl.views[2]
        && currentPanel != panels[2])
        {
            currentPanel.gameObject.SetActive(false);
            currentPanel = panels[2];
            currentPanel.gameObject.SetActive(true);
        }
    }

    void LateUpdate()
    {
        
    }
}
