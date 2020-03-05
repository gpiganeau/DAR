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

    InteractWithItems.Inventory playerInventory;
    public GameObject fireplace;

    bool taskBoolGetWood;

    void Start()
    {
        
    }
    
    void Update()
    {
        string playerInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json");
        playerInventory = JsonUtility.FromJson<InteractWithItems.Inventory>(playerInventoryJSON);

        if (playerInventory.wood > 0)
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
