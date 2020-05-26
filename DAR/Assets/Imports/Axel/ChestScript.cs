using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public Animator animatorChest;
    public GameObject chestPanel;

    void Start()
    {
        animatorChest = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(chestPanel.activeSelf == true)
        {
            animatorChest.SetBool("isOpen", true);
        }
        else animatorChest.SetBool("isOpen", false);
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
