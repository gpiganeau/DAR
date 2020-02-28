using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isSheltered;
    [SerializeField] Transform spawnPosition;

    public bool GetShelteredStatus() {
        return isSheltered;
    }

    public void SetShelteredStatus(bool status) {
        isSheltered = status;
    }

    public void GoHome() {
        gameObject.transform.position = spawnPosition.position;
    }

}
