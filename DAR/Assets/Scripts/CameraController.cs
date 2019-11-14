using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	float xRotation = 0f;   

    public Transform[] views;
    public float transitionSpeed;
    [HideInInspector] public Transform currentView;

    void Start()
    {
        currentView = views[0];
    }
		
    void Update()
    {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		currentView.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeView(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeView(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeView(2);
        }
    }

    void LateUpdate()
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

    public void ChangeView(int viewNumber)
    {
        currentView = views[viewNumber];
    }
}
