using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    Vector3 moveDirection;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float windMultiplier = 1f;
    bool isGrounded;
    public int inWindState;

   
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        /* Blocking movement in one direction
         * Commanded by Directionnal Hazard which sets the windstate*/

        if (inWindState == 1) {
            windMultiplier = 0.3f;
        }
        else if (inWindState == 2) {
            windMultiplier = 1.3f;
        }
        else {
            windMultiplier = 1f;
        }

        controller.Move(move * speed * windMultiplier * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        moveDirection = move.normalized;

    }

    public bool AmIGrounded() {
        return isGrounded;
    }

    IEnumerator GustOfWind(Vector3 direction, int value) {
        float currentValue = value;
        for (int j = 0; j < 4; j++) {
            velocity += direction * value / 4;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 5; i++) { 
            velocity -= direction * (currentValue / 2);
            currentValue -= currentValue / 2;
            Debug.Log(currentValue);
            yield return new WaitForSeconds(.25f);
        }
        Debug.Log(currentValue - currentValue);
        velocity -= direction * currentValue;
    }


    public void velocityChange(Vector3 direction, float value) {
        velocity += direction * value;
    }
    public void velocityBurst(Vector3 direction, int value) {
        StartCoroutine(GustOfWind(direction, value));
    }

    public void RemoveVelocity() {
        //Resets Velocity to zero
        velocity = new Vector3(0, 0, 0);
    }

    public Vector3 getVelocityNormalized() {
        return velocity.normalized;
    }
    public Vector3 getMoveDirection() {
        return moveDirection;
    }
}
