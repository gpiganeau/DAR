using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCraftingInventoryUIData : MonoBehaviour {

    private Text _nb_Wood;
    private Text _nb_Mushroom;
    private int woodValue;
    private int mushroomValue;

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
        woodValue = _hubInventory.wood + playerInventoryManager.GetInventory().CountItem("wood");
        mushroomValue = _hubInventory.mushroom + playerInventoryManager.GetInventory().CountItem("mushroom");
        _nb_Wood.text = "x " + (woodValue);
        _nb_Mushroom.text = "x " + (mushroomValue);
    }

    public int[] GetValues()
    {
        int [] ReturnArray = new int[2];
        ReturnArray[0] = woodValue;
        ReturnArray[1] = mushroomValue;
        return ReturnArray;
    }

    public void CraftItem(string itemName) {
        switch (itemName) {
            case "planche": 
                playerInventoryManager.Consume("wood", 4);
                break;
            case "balise":
                playerInventoryManager.Consume("wood", 2);
                break;
            case "echelle":
                playerInventoryManager.Consume("wood", 6);
                break;
        }        
    }

   
    
}