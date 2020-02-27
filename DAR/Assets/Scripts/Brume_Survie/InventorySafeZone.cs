using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySafeZone : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other) {
        if (player.CompareTag(other.tag)) {
            player.GetComponent<PlayerStatus>().SetShelteredStatus(true) ;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (player.CompareTag(other.tag)) {
            player.GetComponent<PlayerStatus>().SetShelteredStatus(false);
        }
    }

}
