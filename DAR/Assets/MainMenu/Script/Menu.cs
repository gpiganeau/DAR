using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public Button firstButton;
    public GameObject choose;
    public GameObject team;
    public GameObject parametreGraphism;
    public GameObject parametreControls;
    public GameObject parametreSound;
    public GameObject parametreControlsPage1;
    public GameObject parametreControlsPage2;
    public Text changingText;
    public Button loadButton;

    InGameDay loadedDay;
    PlayerInventoryManager.Inventory playerInventory;
    HubInventoryManager.Inventory hubInventory;

    public bool isNew;

    Animator anim;

    public MainMenuToLoadScene loadScenePath;
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (checkForPreviousData() == true)
        {
            loadButton.interactable = true;
        }
    }

    void Update()
    {
        //Revenir en arrière
        if (Input.GetAxis("Cancel") != 0 && anim.GetBool("EstSlide") == true)
        {
            EventSystem.current.SetSelectedGameObject(null);
            anim.SetBool("EstSlide", false);
            anim.SetBool("EstVisible", false);
            choose.SetActive(false);
            team.SetActive(false);
            parametreGraphism.SetActive(false);
            parametreSound.SetActive(false);
            parametreControls.SetActive(false);
        }

        //first selected on move
        if (EventSystem.current.currentSelectedGameObject == null
            && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            && anim.GetBool("EstSlide") == false)
        {
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }

    private bool checkForPreviousData()
    {
        return SaveSystem.previousDataExists();
    }


    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }

    //------------------------------[Partie Lancement]----------------------------------------------------------//

    public void New()
    {
        team.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
        choose.SetActive(true);
        changingText.text = " Nouvelle Partie ";
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
        isNew = true;
        EventSystem.current.SetSelectedGameObject(choose.GetComponentInChildren<Button>().gameObject);
        //anim.SetBool("EstVisiblePartie",true);
    }

    public void Load()
    {
        team.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
        choose.SetActive(true);
        changingText.text = " Continuer ";
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
        isNew = false;
        EventSystem.current.SetSelectedGameObject(choose.GetComponentInChildren<Button>().gameObject);
        //anim.SetBool("EstVisiblePartie",true);

        LoadDayData();
    }

    public void LoadDayData()
    {
        SaveData dayData = SaveSystem.LoadDay();
        string jsonDay = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        loadedDay.day = dayData.day;
        string uploadDay = JsonUtility.ToJson(loadedDay);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json", uploadDay);
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
            loadScenePath.sceneToLoad = 2;
            loadScenePath.GoToIntro();
        }
        else
        {
            //*Reset parce qu'on charge pas encore l'inventaire et tout le reste*
            //RESET ON HUB AND PLAYER INVENTORY
            //USING THE CONSTRUCTOR FUNCTION
            playerInventory = new PlayerInventoryManager.Inventory(2, "PlayerInventory.json");
            hubInventory = new HubInventoryManager.Inventory("HubInventory.json");

            playerInventory.WriteInventory();
            hubInventory.WriteInventory();
            loadScenePath.sceneToLoad = 2;
            loadScenePath.GoToLoad();
        }
        
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
        EventSystem.current.SetSelectedGameObject(parametreGraphism.GetComponentInChildren<Button>().gameObject);
    }

    public void Controls()
    {
        choose.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(true);
        parametreControlsPage1.SetActive(true);
        parametreControlsPage2.SetActive(false);
        team.SetActive(false);
        anim.SetBool("EstSlide",true);
        anim.SetBool("EstVisible", false);
        EventSystem.current.SetSelectedGameObject(parametreControls.GetComponentInChildren<Button>().gameObject);
    }

    public void GoPage2()
    {
        choose.SetActive(false);
        parametreControlsPage1.SetActive(false);
        parametreControlsPage2.SetActive(true);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(true);
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
        EventSystem.current.SetSelectedGameObject(parametreSound.GetComponentInChildren<Button>().gameObject);
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
        EventSystem.current.SetSelectedGameObject(null);
    }
}
