using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCraftingInventoryUIData : MonoBehaviour {

    private Text _nb_Wood;
    private Text _nb_Mushroom;

    [SerializeField] InteractWithItems interactWithItems;
    private InteractWithItems.Inventory playerInventory;

    [SerializeField] private GameObject player;

    private void OnEnable() {
        if (_nb_Wood == null || _nb_Mushroom == null) {
            _nb_Wood = transform.Find("TotalInventoryFrame").transform.Find("nb_Wood").GetComponent<Text>();
            _nb_Mushroom = transform.Find("TotalInventoryFrame").transform.Find("nb_Mushroom").GetComponent<Text>();
        }
        playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");
        
    }

    public void UpdateNumbers(HubInventoryManager.Inventory _hubInventory) {
        playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");
        _nb_Wood.text = "x " + (_hubInventory.wood + playerInventory.wood);
        _nb_Mushroom.text = "x " + (_hubInventory.mushroom + playerInventory.mushroom);
    }

    public void CraftItem(int itemIndex) {
        Debug.Log("Crafting");
        switch (itemIndex) {
            case 0:
                playerInventory = interactWithItems.UseRessources("wood", 2);
                break;
            case 1:
                playerInventory = interactWithItems.UseRessources("wood", 4);
                break;
            case 2:
                playerInventory = interactWithItems.UseRessources("wood", 6);
                break;
        }        
    }

}