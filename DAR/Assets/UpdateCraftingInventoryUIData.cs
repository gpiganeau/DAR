using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCraftingInventoryUIData : MonoBehaviour {

    private Text _nb_Wood;
    private Text _nb_Mushroom;
    private int woodValue;
    private int mushroomValue;
    private int radioValue;

    

    [SerializeField] InteractWithItems interactWithItems;
    [SerializeField] private PlayerInventoryManager playerInventoryManager;
    [SerializeField] private GameObject workBench;
    [SerializeField] private GameObject player;


    private void OnEnable() {
        if (_nb_Wood == null || _nb_Mushroom == null) {
            _nb_Wood = transform.Find("TotalInventoryFrame").transform.Find("nb_Wood").GetComponent<Text>();
            _nb_Mushroom = transform.Find("TotalInventoryFrame").transform.Find("nb_Mushroom").GetComponent<Text>();
        }
    }

    public void UpdateNumbers(HubInventoryManager.Inventory _hubInventory) {
        woodValue = _hubInventory.Count("Bûche") + playerInventoryManager.GetInventory().CountItem("Bûche");
        mushroomValue = _hubInventory.Count("Champignon") + playerInventoryManager.GetInventory().CountItem("Champignon");
        radioValue = _hubInventory.Count("Antenne") + _hubInventory.Count("Générateur") + _hubInventory.Count("Haut-Parleur") + _hubInventory.Count("Potentiomètre");
        _nb_Wood.text = "x " + (woodValue);
        _nb_Mushroom.text = "x " + (mushroomValue);
    }

    public int[] GetValues()
    {
        int [] ReturnArray = new int[3];
        ReturnArray[0] = woodValue;
        ReturnArray[1] = mushroomValue;
        ReturnArray[2] = radioValue;
        return ReturnArray;
    }

    public void CraftItem(Item item) {
        switch (item._name) {
            case "Planche": 
                playerInventoryManager.Consume("Bûche", 4);
                workBench.GetComponent<SpawningCraftedItem>().SpawnCraft(item);
                break;
            case "Balise":
                playerInventoryManager.Consume("Bûche", 2);
                workBench.GetComponent<SpawningCraftedItem>().SpawnCraft(item);
                break;
            /*case "echelle":
                playerInventoryManager.Consume("Bûche", 6);
                break;*/
            case "Radio Complete":
                playerInventoryManager.Consume("Antenne", 1);
                playerInventoryManager.Consume("Générateur", 1);
                playerInventoryManager.Consume("Haut-Parleur", 1);
                playerInventoryManager.Consume("Potentiomètre", 1);
                workBench.GetComponent<SpawningCraftedItem>().SpawnCraft(item);
                break;
        }        
    }

   
    
}