using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ChecklistScript : MonoBehaviour
{
    public Image taskCheck1;
    public Image taskCheck2;
    public Image taskCheck3;
    public Image taskCheck4;

    PlayerInventoryManager playerInventoryManager;
    public GameObject fireplace;

    bool taskBoolGetWood;

    void Start()
    {
        
    }
    
    void Update()
    {

        if (playerInventoryManager.GetInventory().CountItem("wood") > 0)
        {
            taskBoolGetWood = true;
        }

        if (taskBoolGetWood) {
            taskCheck1.gameObject.SetActive(true);
        }



        if (fireplace.activeSelf)
        {
            taskCheck4.gameObject.SetActive(true);
        }
    }
}
