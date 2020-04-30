using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDuringNightTime : MonoBehaviour
{
    [SerializeField] private GameObject openedDoor;
    
    public void NightTimeLock() {
        if (openedDoor.activeSelf) {
            openedDoor.GetComponent<DoorScript>().Open(true);
        }
    }

}
