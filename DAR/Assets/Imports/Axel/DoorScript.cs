using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator animatorDoor;
    void Start()
    {
        animatorDoor = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        
    }

    public void Open() {
        if (!animatorDoor.GetBool("isOpen"))
        {
            animatorDoor.SetBool("isOpen", true);
        }

        else
        {
            animatorDoor.SetBool("isOpen", false);
        }
    }



}
