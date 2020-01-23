using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public enum CameraMode {Player, Cinematic};
    public CameraMode myCamMode = CameraMode.Player;
    public Transform[] views;
    public float transitionSpeed;
    [HideInInspector] public Transform currentView;

    void Start()
    {
        currentView = views[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (currentView == views[0])
            {
                ChangeView(views[1], CameraMode.Cinematic);
            }
            else ChangeView(views[0], CameraMode.Player);
        }
    }

    void LateUpdate()
    {
        if (myCamMode == CameraMode.Cinematic)
        {
            //Lerp position
            transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngle = new Vector3 (
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );

            transform.eulerAngles = currentAngle;
        }
    }


    public void StartCinematic() {
        ChangeView(views[1], CameraMode.Cinematic);
    }


    public void ChangeView(Transform newView, CameraMode newCameraMode)
    {
        if (newCameraMode == CameraMode.Player)
        {
            Animator cinemAnim = currentView.gameObject.GetComponentInParent<Animator>();
            cinemAnim.SetBool("Animate", false);
            transform.position = newView.position;
            gameObject.GetComponent<mouseLook>().enabled = true;
        }

        else if (newCameraMode == CameraMode.Cinematic)
        {
            gameObject.GetComponent<mouseLook>().enabled = false;
            Animator cinemAnim = newView.gameObject.GetComponentInParent<Animator>();
            cinemAnim.SetBool("Animate", true);
        }

        myCamMode = newCameraMode;
        currentView = newView;
    }
}
