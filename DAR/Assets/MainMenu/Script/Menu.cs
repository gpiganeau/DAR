using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    public GameObject choose;
    public GameObject team;
    public GameObject parametreGraphism;
    public GameObject parametreControls;
    public GameObject parametreSound;
    public Text changingText;

    InGameDay loadedDay;
    PlayerInventoryManager.Inventory playerInventory;
    HubInventoryManager.Inventory hubInventory;

    public bool isNew;

    Animator anim;

    public MainMenuToLoadScene loadScenePath;
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }


    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }

    //------------------------------[Partie Lancement]----------------------------------------------------------//

    public void New()
    {
        //SceneManager.LoadScene("Scene2");
        team.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
        choose.SetActive(true);
        changingText.text = " Nouvelle Partie ";
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
        isNew = true;
        //anim.SetBool("EstVisiblePartie",true);
    }

    public void Load()
    {
        //SceneManager.LoadScene("Scene2");
        team.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
        choose.SetActive(true);
        changingText.text = " Continuer ";
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
        isNew = false;
        //anim.SetBool("EstVisiblePartie",true);
    }

    public void Play()
    {
        if (isNew)
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
        loadScenePath.sceneToLoad = 2;
        loadScenePath.GoToLoad();
    }

//------------------------------[Partie Options]------------------------------------------------------------//

    public void Graphism()
    {
        choose.SetActive(false);
        parametreGraphism.SetActive(true);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
        team.SetActive(false);
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
    }

    public void Controls()
    {
        choose.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(true);
        team.SetActive(false);
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
    }

    public void Sound()
    {
        choose.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(true);
        parametreControls.SetActive(false);
        team.SetActive(false);
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
    }

//------------------------------[Partie Quitter]------------------------------------------------------------//

    public void QuitGame()
    {
        Application.Quit();
    }

//------------------------------[Partie Équipe]------------------------------------------------------------//

    public void Team()
    {
        choose.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
        team.SetActive(true);
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", true);
    }
}
