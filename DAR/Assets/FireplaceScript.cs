using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FireplaceScript : MonoBehaviour
{
    public GameObject fireplaceOn;
    public GameObject fireParticles;
    InteractWithItems.Inventory playerInventory;
    

    void Start()
    {
 
    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        string playerInventoryJSON = File.ReadAllText(Application.dataPath + "/JSONFiles/PlayerInventory.json");
        playerInventory = JsonUtility.FromJson<InteractWithItems.Inventory>(playerInventoryJSON);
        if (other.tag == "Player" && playerInventory.wood >= 3)
        {
            if (fireplaceOn.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    fireplaceOn.gameObject.SetActive(true);
                    fireParticles.gameObject.SetActive(true);
                    playerInventory.wood -= 3;
                    string uploadInventory = JsonUtility.ToJson(playerInventory);
                    File.WriteAllText(Application.dataPath + "/JSONFiles/PlayerInventory.json", uploadInventory);
                }
            }
        }
    }
}
