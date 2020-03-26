using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FireplaceScript : MonoBehaviour
{
    public GameObject fireplaceOn;
    public GameObject fireParticles;
    public GameObject player;
    HubInventoryManager hubInventoryManager;

    InteractWithItems.Inventory playerInventory;
    

    void Start()
    {
 
    }

    void Update()
    {

    }

    public InteractWithItems.Inventory Light() {
        playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");

        if (fireplaceOn.activeSelf == false) {
            fireplaceOn.gameObject.SetActive(true);
            fireParticles.gameObject.SetActive(true);
            player.GetComponent<InteractWithItems>().UseRessources("wood", 3);
        }
        return playerInventory;
    }
}
