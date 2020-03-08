using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float stickSensitivity = 1000f;

    public Transform playerBody;

    float xRotation = 0f;
    public bool ignore;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ignore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ignore) {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            mouseX += Input.GetAxis("RightStick X") * stickSensitivity * Time.deltaTime;
            mouseY -= Input.GetAxis("RightStick Y") * stickSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 60f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }


        
    }
}
