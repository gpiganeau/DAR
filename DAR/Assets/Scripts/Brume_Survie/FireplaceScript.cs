using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FireplaceScript : MonoBehaviour
{
    public GameObject fireplaceOn;
    public GameObject fireParticles;
    InteractWithItems.Inventory hubInventory;
    InteractWithItems.Inventory playerInventory;
    

    void Start()
    {
 
    }

    void Update()
    {

    }

    public InteractWithItems.Inventory Light() {
        string hubInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json");
        hubInventory = JsonUtility.FromJson<InteractWithItems.Inventory>(hubInventoryJSON);
        string playerInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json");
        playerInventory = JsonUtility.FromJson<InteractWithItems.Inventory>(playerInventoryJSON);
        if (fireplaceOn.activeSelf == false) {
            int totalWood = 3;
            fireplaceOn.gameObject.SetActive(true);
            fireParticles.gameObject.SetActive(true);
            while (playerInventory.wood > 0 && totalWood > 0) {
                playerInventory.wood -= 1;
                totalWood -= 1;
            }
            hubInventory.wood -= totalWood;
            string uploadHubInventory = JsonUtility.ToJson(hubInventory);
            string uploadPlayerInventory = JsonUtility.ToJson(playerInventory);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json", uploadHubInventory);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json", uploadPlayerInventory);
        }
        return playerInventory;
    }
}
