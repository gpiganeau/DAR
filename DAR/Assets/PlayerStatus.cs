using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isSheltered;

    public bool GetShelteredStatus() {
        return isSheltered;
    }

    public void SetShelteredStatus(bool status) {
        isSheltered = status;
    }
}
