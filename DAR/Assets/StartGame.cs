using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    InGameDay loadedDay;
    // Start is called before the first frame update
    void Start()
    {
        string jsonDay = File.ReadAllText(Application.dataPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        loadedDay.day = 1;
        string uploadDay = JsonUtility.ToJson(loadedDay);
        File.WriteAllText(Application.dataPath + "/JSONFiles/CurrentDay.json", uploadDay);

        //INSERT HERE A RESET ON HUB AND PLAYER INVENTORY
        //USE THE CONSTRUCTOR FUNCTION




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
