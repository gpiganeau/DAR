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
        hubInventory = InteractWithItems.Inventory.ReadInventory("HubInventory.json");
        playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");

        if (fireplaceOn.activeSelf == false) {
            int totalWood = 3;
            fireplaceOn.gameObject.SetActive(true);
            fireParticles.gameObject.SetActive(true);
            while (playerInventory.wood > 0 && totalWood > 0) {
                playerInventory.wood -= 1;
                totalWood -= 1;
            }
            hubInventory.wood -= totalWood;

            hubInventory.WriteInventory();
            playerInventory.WriteInventory();
        }
        return playerInventory;
    }
}
