using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSounds : MonoBehaviour
{
    private Pause pauseObject;
    private float idleTimer = 0f;
    private int idleLimit = 300;
    void Start()
    {
        pauseObject = GetComponent<Pause>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!Input.anyKeyDown && !pauseObject.isDead && !pauseObject.gamePaused)
        {
            idleTimer = idleTimer + 1;
        } else {
            idleTimer = 0;
        }
        if(idleTimer == idleLimit) {
            FMOD.Studio.EventInstance inventorySound = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Idle");
            inventorySound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
            inventorySound.start();
            inventorySound.release();
            idleLimit = Random.Range(300, 600);
            idleTimer = 0;
        }
    }
}
