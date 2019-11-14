using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Animator anim;

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
	   
	void Start()
	{
		anim = GetComponent<Animator> ();
	}
    // Update is called once per frame
    void Update()
    {
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
			anim.SetBool ("Marcher", true);
		}
		//
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
			
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

		if (Input.GetButtonDown ("Jump") && isGrounded) {
			anim.SetBool ("Jump", true);
			velocity.y = Mathf.Sqrt (jumpHeight * -2f * gravity);
	
		} else
		{
			anim.SetBool ("Jump", false);

		}

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

		if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical")) {
			anim.SetBool ("Marcher", false);
		}
    }
}
