using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float stickSensitivity = 1000f;

    public Transform playerBody;

    float xRotation = 0f;
    bool ignore;

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


        if (Input.GetKeyDown(KeyCode.Tab)) {
            Debug.Log("1");
            if (Cursor.lockState == CursorLockMode.Locked) {
                Debug.Log("2");
                Cursor.lockState = CursorLockMode.None;
                ignore = true;
            }
            else if (Cursor.lockState == CursorLockMode.None) {
                Debug.Log("3");
                Cursor.lockState = CursorLockMode.Locked;
                ignore = false;
            }
        }
    }
}
