using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isSheltered;
    public bool isWarm;
    [SerializeField] GameObject fireplace;
    [SerializeField] Transform spawnPosition;

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

}
