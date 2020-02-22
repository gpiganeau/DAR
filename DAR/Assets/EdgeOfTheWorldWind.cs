using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeOfTheWorldWind : MonoBehaviour
{
    [SerializeField] PlayerMovement mainCharacter;
    [SerializeField] Vector3 windDirection;

    private void OnTriggerStay(Collider other) {
        mainCharacter.velocityChange(windDirection, 0.2f);
    }

    private void OnTriggerExit(Collider other) {
        mainCharacter.RemoveVelocity();
    }
}
