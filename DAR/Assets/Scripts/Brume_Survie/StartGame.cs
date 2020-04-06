using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    InGameDay loadedDay;
    PlayerInventoryManager.Inventory playerInventory;
    HubInventoryManager.Inventory hubInventory;
    // Start is called before the first frame update
    void GameStart()
    {
        string jsonDay = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        loadedDay.day = 1;
        string uploadDay = JsonUtility.ToJson(loadedDay);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json", uploadDay);

        //RESET ON HUB AND PLAYER INVENTORY
        //USING THE CONSTRUCTOR FUNCTION
        playerInventory = new PlayerInventoryManager.Inventory(2, "PlayerInventory.json");
        hubInventory = new HubInventoryManager.Inventory("HubInventory.json");

        playerInventory.WriteInventory();
        hubInventory.WriteInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }
}
