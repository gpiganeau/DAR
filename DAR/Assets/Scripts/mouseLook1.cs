using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook1 : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;  
	public Transform playerBody;

	public Transform[] views;
	public float transitionSpeed;
	[HideInInspector] public Transform currentView;

    // Start is called before the first frame update
    void Start()
    {
		currentView = views[0];
    	Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			ChangeView(0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			ChangeView(1);
		}


    }

	void LateUpdate()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		//Lerp position
		transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

		Vector3 currentAngle = new Vector3 (
			Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
			Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
			Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
		);
		transform.eulerAngles = currentAngle;

		if (currentView == views[0]) {
			xRotation -= mouseY;
			xRotation = Mathf.Clamp (xRotation, -45f, 45f);
			transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

		}
		else {
			xRotation -= mouseY;
			xRotation = Mathf.Clamp (xRotation, -45f, 45f);
			transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		}

		playerBody.Rotate(Vector3.up * mouseX);

	}

	public void ChangeView(int viewNumber)
	{
		currentView = views[viewNumber];
	}
}
