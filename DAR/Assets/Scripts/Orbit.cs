using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public float mouseSensitivity = 100f;

	public Transform playerBody;
	public Vector3 offset;
	float xRotation = 0f;  
	float yRotation = 0f;
	Rigidbody playerRB;    

	// Start is called before the first frame update
	void Start()
	{
		playerRB = playerBody.GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
		//offset = new Vector3 (playerBody.position.x, playerBody.position.y + 8.0f, playerBody.position.z + 7.0f);
	}

    // Update is called once per frame
    void Update()
    {
		if (playerRB.velocity.sqrMagnitude > 0.1f) { 
		Quaternion lookR = Quaternion.LookRotation(playerRB.velocity);
		Vector3 rotatedOffset = lookR * offset;
		transform.position = playerBody.position + rotatedOffset;
		transform.LookAt(playerBody);
		}
	}

	void LateUpdate()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		//transform.position = playerBody.position + offset;
		xRotation -= mouseY;
		yRotation -= mouseX;
		//xRotation = Mathf.Clamp(xRotation, -yRotation, 90f);

		//transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0f);
		//playerBody.Rotate(Vector3.up * mouseX);
		//transform.RotateAround(playerBody.position, Vector3.up, mouseX);

	}
}
