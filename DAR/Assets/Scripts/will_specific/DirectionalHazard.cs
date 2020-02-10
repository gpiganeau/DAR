using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalHazard : MonoBehaviour
{
    [SerializeField] CharacterController playerCharacter;
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] Vector3 WindDirection;
    [SerializeField] Vector3 RandomWindDirection;
    [SerializeField] UnstableWeather unstableWeatherScript;
    public bool windEnable = false;
    bool on;
    // Start is called before the first frame update
    void Start()
    {
        if (windEnable) {
            playerMovementScript.velocityChange(-WindDirection, 5);
        }
        on = windEnable;
    }

    // Update is called once per frame
    void Update() {

        if (windEnable) {
            WindSwitchON();
        }
        else {
            WindSwitchOFF();
        }

        void WindSwitchON(){
            if (!on) {
                playerMovementScript.velocityChange(-WindDirection, 5);
                on = true;
            }
        }

        void WindSwitchOFF() {
            if (on) {
                playerMovementScript.RemoveVelocity();
                on = false;
            }
        }

        float dotValue = Vector3.Dot(WindDirection, playerMovementScript.getMoveDirection());

        /*if (dotValue > 0.35f) {
            playerMovementScript.inWindState = 1;
        }
        else if (dotValue < -0.65f) {
            playerMovementScript.inWindState = 2;
        }
        else {
            playerMovementScript.inWindState = 0;
        }*/

        if (Input.GetKeyDown("a")){
            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 0f);
            int strength = Random.Range(10, 30);
            RandomWindDirection = new Vector3(x, 0, z); 
            playerMovementScript.velocityBurst(RandomWindDirection, strength);
            unstableWeatherScript.StartChangingWindCoroutine(RandomWindDirection, strength);
        }

    }
}
