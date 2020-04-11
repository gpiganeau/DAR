using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator animatorDoor;

    [FMODUnity.EventRef]
    public string doorEvent = "";
    public FMOD.Studio.EventInstance doorSound;

    void Start()
    {
        animatorDoor = gameObject.GetComponent<Animator>();
        doorSound = FMODUnity.RuntimeManager.CreateInstance(doorEvent);
        doorSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
    }
    
    void Update()
    {
        
    }

    public void Open() {
        if (!animatorDoor.GetBool("isOpen"))
        {
            animatorDoor.SetBool("isOpen", true);
            
            doorSound.setParameterByName("door_Open", 1);
            doorSound.start();
        }

        else
        {
            animatorDoor.SetBool("isOpen", false);

            doorSound.setParameterByName("door_Open", 0);
            doorSound.start();
        }
    }



}
