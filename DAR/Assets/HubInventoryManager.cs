using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HubInventoryManager : MonoBehaviour
{
    public static bool initializeWithLoad = true;

    public GameObject[] inventorySlots;

    private Inventory hubInventory;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject woodStorage;
    [SerializeField] private GameObject hubUI;
    [SerializeField] private GameObject craftUI;
    //[SerializeField] private

    void Start()
    {
        if (initializeWithLoad) {
            hubInventory = new Inventory("HubInventory.json");
        }
        else {
            hubInventory = Inventory.ReadInventory("HubInventory.JSON");
        }
       
        hubInventory.SetManagerReference(this);
        hubInventory.WriteInventory();
        for (int i = 0; i < inventorySlots.Length; i++) {
            inventorySlots[i] = GameObject.Find("Inventory_Hub_Panel").transform.GetChild(i).gameObject;
        }
    }
  
    void Update()
    {

    }

    public Inventory GetHubInventory() {
        return hubInventory;
    }

    public void ChangeValue(Item item, int itemValue) {
        bool found = false;
        for (int i = 0; i < hubInventory.content.Count; i++) {
            if (hubInventory.content[i]._name == item._name) {
                hubInventory.amount[i] += itemValue;
                found = true;
            }
        }
        if (!found) {
            hubInventory.content.Add(item);
            hubInventory.amount.Add(itemValue);
        }
        UpdateAll();
    }



    public void UpdateAll() {
        woodStorage.GetComponent<woodStorage>().Show(hubInventory.Count("wood"));
        hubUI.GetComponent<UpdateInventoryHubUIData>().UpdateValues(hubInventory);
        craftUI.GetComponent<UpdateCraftingInventoryUIData>().UpdateNumbers(hubInventory);
        hubInventory.WriteInventory();
    }

    public void ItemTransferHubToPlayer(Item item) {
        hubInventory.Remove(item, 1);
        player.GetComponent<PlayerInventoryManager>().AddItem(item);
        UpdateAll();
    }

    public class Inventory
    {
        public List<Item> content;
        public List<int> amount;

        public string filename;
        public HubInventoryManager hubInventoryManager;


        public Inventory(string _filename)
        {
            content = new List<Item>();
            amount = new List<int>();
           
            filename = _filename;
        }

        public void SetManagerReference(HubInventoryManager _hubInventoryManager) {
            hubInventoryManager = _hubInventoryManager;
        }

        public static Inventory ReadInventory(string filename){
            string InventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename);
            Inventory returnInventory = JsonUtility.FromJson<Inventory>(InventoryJSON);
            return returnInventory;
        }

        public void WriteInventory() {
            string uploadInventory = JsonUtility.ToJson(this);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename, uploadInventory);
        }

        public int Count(string item_type) {
            int nullValue = 0;
            for(int i = 0; i < content.Count; i++) {
                if (content[i]._name == item_type) {
                    return amount[i];
                }
            }
            return nullValue;
        }

        public void Remove(Item item, int number) {
            for (int i = 0; i < content.Count; i++) {
                if (content[i]._name == item._name) {
                    amount[i] -= number;
                }
            }
        }

    }



}
