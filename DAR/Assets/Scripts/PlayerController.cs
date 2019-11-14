using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody reggiebody;
    static float setMoveSpeed = 4f;
    static float setCamSpeed = 1.5f;
    public float viewRange = 5.0f;
    public Vector3 veloc;
    public Camera cam;

    public float axisX;
    public float axisY;
    public float axis4;
    public float axis5;

    Transform cameraTransform;

    void Start()
    {
        reggiebody = gameObject.GetComponent<Rigidbody>();
        veloc = new Vector3(0, 0, 0);
        cam = GetComponentInChildren<Camera>();
        cameraTransform = cam.transform;
    }

    void Update()
    {
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");

        var h = Input.GetAxis("Horizontal") * Time.deltaTime * setMoveSpeed;
        var v = Input.GetAxis("Vertical") * Time.deltaTime * setMoveSpeed;

        transform.Translate(h, 0, v);

        //forward
        /*if (Input.GetAxis ("Vertical") >= 0.8 || Input.GetKey (KeyCode.Z)) {
			veloc.z = setMoveSpeed;
		}
		//backward
		if (Input.GetAxis ("Vertical") <= -0.8 || Input.GetKey (KeyCode.S)) {
			veloc.z = -setMoveSpeed;
		}
		//left
		if (Input.GetAxis ("Horizontal") <= -0.8 || Input.GetKey (KeyCode.Q)) {
			veloc.x = -setMoveSpeed;
		}
		//right
		if (Input.GetAxis ("Horizontal") >= 0.8 || Input.GetKey (KeyCode.D)) {
			veloc.x = setMoveSpeed;
		}*/

        axis4 = Input.GetAxis("Cam X");
        axis5 = Input.GetAxis("Cam Y");

        //ici, l'avatar tourne (Axe X)
        if (axis4 >= 0.8)
        {
            transform.Rotate(Vector3.up * setCamSpeed);
        }
        if (axis4 <= -0.8)
        {
            transform.Rotate(Vector3.up * -setCamSpeed);
        }
        //ici, la caméra tourne (Axe Y)
        if (axis5 >= 0.8)
        {
            cam.transform.Rotate(Vector3.left * setCamSpeed * axis5);
        }
        if (axis5 <= -0.8)
        {
            cam.transform.Rotate(Vector3.left * setCamSpeed * axis5);
        }

        //Section permettant de limiter les mouvements de la caméra sur l'axe Y, histoire de ne pas finir la tête en bas.
        float x = cameraTransform.localEulerAngles.x;
        if (x > viewRange && x < 340)
        {
            if (x > viewRange && x < (viewRange + 50))
            {
                cameraTransform.localEulerAngles = new Vector3(viewRange, cameraTransform.localEulerAngles.y, 0);
            }
            else if (x > 290 && x < 340)
            {
                cameraTransform.localEulerAngles = new Vector3(340, cameraTransform.localEulerAngles.y, 0);
            }
        }

        ApplyVelocity();
        veloc = new Vector3(0, 0, 0);
    }

    void Move()
    {
        //
    }

    void ApplyVelocity()
    {
        reggiebody.velocity = veloc;
    }
}
