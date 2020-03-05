using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    InGameDay loadedDay;
    InteractWithItems.Inventory playerInventory;
    InteractWithItems.Inventory hubInventory;
    // Start is called before the first frame update
    void Start()
    {
        string jsonDay = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        loadedDay.day = 1;
        string uploadDay = JsonUtility.ToJson(loadedDay);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json", uploadDay);

        //RESET ON HUB AND PLAYER INVENTORY
        //USING THE CONSTRUCTOR FUNCTION
        playerInventory = new InteractWithItems.Inventory(2);
        hubInventory = new InteractWithItems.Inventory(1000);

        string uploadPlayerInv = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json", uploadPlayerInv);

        string uploadHubInv = JsonUtility.ToJson(hubInventory);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json", uploadHubInv);


        SceneManager.LoadScene(1);
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
