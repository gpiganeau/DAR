using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrailOnTerrain : MonoBehaviour
{
    private object playerPositionOnTerrain;
    bool[,] playerHasPassed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPositionOnTerrain = getPlayerPosition();
        setPlayerHasPassed(playerPositionOnTerrain);
        updateVisibleTrail();
    }

    private void updateVisibleTrail() {
        throw new NotImplementedException();
    }

    private void setPlayerHasPassed(object playerPositionOnTerrain) {
        throw new NotImplementedException();
    }

    private object getPlayerPosition() {
        throw new NotImplementedException();
    }
}
