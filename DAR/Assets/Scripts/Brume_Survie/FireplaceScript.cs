using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FireplaceScript : MonoBehaviour
{
    public GameObject fireplaceOn;
    public GameObject fireParticles;
    InteractWithItems.Inventory hubInventory;
    

    void Start()
    {
 
    }

    void Update()
    {

    }

    public void Light() {
        string playerInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json");
        hubInventory = JsonUtility.FromJson<InteractWithItems.Inventory>(playerInventoryJSON);
        if (fireplaceOn.activeSelf == false) {

            fireplaceOn.gameObject.SetActive(true);
            fireParticles.gameObject.SetActive(true);
            hubInventory.wood -= 3;
            string uploadInventory = JsonUtility.ToJson(hubInventory);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json", uploadInventory);
            
        }
    }
}
