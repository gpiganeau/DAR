using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCraftingInventoryUIData : MonoBehaviour {

    private Text _nb_Wood;
    private Text _nb_Mushroom;

    [SerializeField] InteractWithItems interactWithItems;
    [SerializeField] private PlayerInventoryManager playerInventoryManager;

    [SerializeField] private GameObject player;

    private void OnEnable() {
        if (_nb_Wood == null || _nb_Mushroom == null) {
            _nb_Wood = transform.Find("TotalInventoryFrame").transform.Find("nb_Wood").GetComponent<Text>();
            _nb_Mushroom = transform.Find("TotalInventoryFrame").transform.Find("nb_Mushroom").GetComponent<Text>();
        }
    }

    public void UpdateNumbers(HubInventoryManager.Inventory _hubInventory) {
        _nb_Wood.text = "x " + (_hubInventory.wood + playerInventoryManager.GetInventory().CountItem("wood"));
        _nb_Mushroom.text = "x " + (_hubInventory.mushroom + playerInventoryManager.GetInventory().CountItem("mushroom"));
    }

    public void CraftItem(int itemIndex) {
        switch (itemIndex) {
            case 0:
                playerInventoryManager.Consume("wood", 2);
                break;
            case 1:
                playerInventoryManager.Consume("wood", 4);
                break;
            case 2:
                playerInventoryManager.Consume("wood", 6);
                break;
        }        
    }

}