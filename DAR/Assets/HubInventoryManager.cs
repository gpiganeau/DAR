using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HubInventoryManager : MonoBehaviour
{
    private Inventory hubInventory;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject woodStorage;
    [SerializeField] private GameObject hubUI;
    [SerializeField] private GameObject craftUI;
    //[SerializeField] private

    void Start()
    {
        hubInventory = Inventory.ReadInventory("HubInventory.JSON");
        hubInventory.SetManagerReference(this);
        hubInventory.WriteInventory();
        UpdateAll();
    }
  
    void Update()
    {

    }

    public Inventory GetHubInventory() {
        return hubInventory;
    }

    public void ChangeValue(string itemName, int itemValue) {
        switch (itemName) {
            case "wood":
                hubInventory.wood += itemValue;
                break;
        }
        UpdateAll();
    }

    public void UpdateAll() {
        woodStorage.GetComponent<woodStorage>().Show(hubInventory.wood);
        hubUI.GetComponent<UpdateInventoryHubUIData>().UpdateValues(hubInventory);
        craftUI.GetComponent<UpdateCraftingInventoryUIData>().UpdateNumbers(hubInventory);
        hubInventory.WriteInventory();
    }


    public class Inventory
    {
        
        public int mushroom;
        public int fish;
        public int wood;
        public int waterRation;

        public string filename;
        public HubInventoryManager hubInventoryManager;


        public Inventory(string _filename)
        {
            mushroom = 0;
            fish = 0;
            wood = 0;
            waterRation = 0;
           
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

        public void DepositInInventory(Inventory target) {
            target.mushroom += mushroom;
            target.waterRation += waterRation;
            target.fish += fish;
            target.wood += wood;

            target.WriteInventory();
        }

    }



}
