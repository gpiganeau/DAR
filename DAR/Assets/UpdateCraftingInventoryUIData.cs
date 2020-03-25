using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCraftingInventoryUIData : MonoBehaviour {

    private Text _nb_Wood;
    private Text _nb_Mushroom;

    private InteractWithItems.Inventory playerInventory;
    private InteractWithItems.Inventory hubInventory;

    [SerializeField] private GameObject player;

    private void OnEnable() {
        _nb_Wood = transform.Find("TotalInventoryFrame").transform.Find("nb_Wood").GetComponent<Text>();
        _nb_Mushroom = transform.Find("TotalInventoryFrame").transform.Find("nb_Mushroom").GetComponent<Text>();
        hubInventory = InteractWithItems.Inventory.ReadInventory("HubInventory.json");
        playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");
        _nb_Wood.text = "x " + (hubInventory.wood + playerInventory.wood);
        _nb_Mushroom.text = "x " + (hubInventory.mushroom + playerInventory.mushroom);
    }

    private void UpdateNumbers() {
        hubInventory = InteractWithItems.Inventory.ReadInventory("HubInventory.json");
        _nb_Wood.text = "x " + (hubInventory.wood + playerInventory.wood);
        _nb_Mushroom.text = "x " + (hubInventory.mushroom + playerInventory.mushroom);
    }

    public void CraftItem(int itemIndex) {
        Debug.Log("Crafting");
        switch (itemIndex) {
            case 0:
                playerInventory = playerInventory.Consume("wood", 2);
                player.GetComponent<InteractWithItems>().UpdateWoodStorage();
                UpdateNumbers();
                break;
            case 1:
                playerInventory = playerInventory.Consume("wood", 4);
                player.GetComponent<InteractWithItems>().UpdateWoodStorage();
                UpdateNumbers();
                break;
            case 2:
                playerInventory = playerInventory.Consume("wood", 6);
                player.GetComponent<InteractWithItems>().UpdateWoodStorage();
                UpdateNumbers();
                break;
        }
        
    }

}