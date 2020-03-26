using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HubInventoryManager : MonoBehaviour
{
    public Inventory hubInventory;
    void Start()
    {
        hubInventory = new Inventory("TestHubInventory.JSON");
        hubInventory.WriteInventory();
    }
  
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Comma))
        {
            Debug.Log("Wood : " + hubInventory.wood);
        }
    }

    public class Inventory
    {
        
        public int mushroom;
        public int fish;
        public int wood;
        public int waterRation;

        public string filename;


        public Inventory(string _filename)
        {
            mushroom = 0;
            fish = 0;
            wood = 0;
            waterRation = 0;
           
            filename = _filename;
        }

        public void ChangeValue (string itemName, int itemValue)
        {

        }

        public static Inventory ReadInventory(string filename)
        {
            string InventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename);
            Inventory returnInventory = JsonUtility.FromJson<Inventory>(InventoryJSON);
            return returnInventory;
        }

        public void WriteInventory()
        {
            string uploadInventory = JsonUtility.ToJson(this);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename, uploadInventory);
        }

        public void DepositInInventory(Inventory target)
        {
            target.mushroom += mushroom;
            target.waterRation += waterRation;
            target.fish += fish;
            target.wood += wood;

            target.WriteInventory();

        }

    }



}
