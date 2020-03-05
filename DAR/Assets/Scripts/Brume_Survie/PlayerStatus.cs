using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private bool isSheltered;
    private bool isWarm;
    private bool isResting;
    [SerializeField] GameObject fireplace;
    [SerializeField] Transform spawnPosition;
    [SerializeField] private GameObject hut;

    public bool GetShelteredStatus() {
        return isSheltered;
    }

    public void SetShelteredStatus(bool status) {
        isSheltered = status;
    }

    private void Update() {
        if (fireplace.GetComponent<FireplaceScript>().fireplaceOn.activeSelf) {
            isWarm = true;
        }
        else {
            isWarm = false;
        }
    }

    public void GoHome() {
        gameObject.transform.position = spawnPosition.position;
    }

    public bool GetWarmStatus() {
        return isWarm;
    }
    
    public void SetIsRestingStatus(bool status) {
        isResting = status;
        if (status) {
            hut.GetComponent<LockDuringNightTime>().NightTimeLock();
        }
    }

    public bool GetIsRestingStatus() {
        return isResting;
    }

}
