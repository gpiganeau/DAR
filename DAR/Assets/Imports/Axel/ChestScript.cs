using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public Animator animatorChest;

    void Start()
    {
        animatorChest = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Open() {
        if (!animatorChest.GetBool("isOpen")) {
            animatorChest.SetBool("isOpen", true);
        }

        else {
            animatorChest.SetBool("isOpen", false);
        }
        
    }



}
