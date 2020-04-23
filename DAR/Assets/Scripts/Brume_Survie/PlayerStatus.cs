using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private bool isSheltered;
    private bool isWarm;
    private bool isResting;
    private int freezingLevel;
    [SerializeField] GameObject fireplace;
    [SerializeField] Transform spawnPosition;
    [SerializeField] private GameObject hut;

    private void Start() {
        freezingLevel = 0;
        UpdatePlayerSpeed();
    }

    public int GetFreezingLevel() {
        return freezingLevel;
    }

    public int IncrementFreezingLevel() {
        freezingLevel = Mathf.Clamp(freezingLevel + 1, 0, 2);
        UpdatePlayerSpeed();
        return freezingLevel;
    }

    public int ResetFreezingLevel() {
        freezingLevel = 0;
        return 0;
    }

    public bool GetShelteredStatus() {
        return isSheltered;
    }

    public void SetShelteredStatus(bool status) {
        isSheltered = status;
    }

    private void Update() {
        if (fireplace.GetComponent<FireplaceScript>().fireParticles.activeSelf) {
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

    public void UpdatePlayerSpeed() {
        if (freezingLevel == 0) {
            gameObject.GetComponent<PlayerMovement>().SetSpeed(10);
        }
        else {
            gameObject.GetComponent<PlayerMovement>().SetSpeed(6);
        }
    }
        

}
