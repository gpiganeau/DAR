using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class template : MonoBehaviour
{
    [SerializeField] private mouseLook despoMouseLook;

    public GameObject over;
    public Text cause;
    public Text description;

    InGameDay loadedDay;
    PlayerInventoryManager.Inventory playerInventory;
    HubInventoryManager.Inventory hubInventory;

    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }


    // Start is called before the first frame update
    void Start()
    {
        despoMouseLook = GetComponentInChildren<mouseLook>();
        over.SetActive(false);    
    }

    void Update()
    {
    }

    public void Famine()
    {
        GetComponent<Pause>().isDead =true;
        over.SetActive(true);
        cause.text = "Famine";
        cause.color = new Color(219,164,57);
        Debug.Log(cause.color);
        description.text ="Un trop grand manque de nourriture peut entraîner de très nombreuses maladies, souvent liées à des carences nutritionnelles. Ce manque peut provoquer des conséquences plus graves, comme un état de faiblesse psychologique ou bien la mort.";
        Cursor.lockState = CursorLockMode.None;
        despoMouseLook.ignore = true;
        Time.timeScale = 0;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CameraController>().enabled = false;
        GetComponentInChildren<Headbobber>().enabled = false;
        GetComponent<PlayerInventoryManager>().enabled = false;
    }

    public void Hypothermie()
    {
        GetComponent<Pause>().isDead =true;
        over.SetActive(true);
        cause.text = "Hypothermie";
        cause.color = new Color(0,255,234);
        description.text ="Sans moyen de se réchauffer, un grand froid provoque un trouble du rythme cardiaque, il ralentit puis arrête une à une toutes les fonctions vitales jusqu'à introduire un état de mort apparente, puis une mort réelle.";
        Cursor.lockState = CursorLockMode.None;
        despoMouseLook.ignore = true;
        Time.timeScale = 0;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CameraController>().enabled = false;
        GetComponentInChildren<Headbobber>().enabled = false;
        GetComponent<PlayerInventoryManager>().enabled = false;
    }

    public void Victoire() {
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("OutroScene");
    }


        public void Play()
    {

            string jsonDay = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json");
            loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
            loadedDay.day = 1;
            string uploadDay = JsonUtility.ToJson(loadedDay);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json", uploadDay);

            playerInventory = new PlayerInventoryManager.Inventory(2, "PlayerInventory.json");
            hubInventory = new HubInventoryManager.Inventory("HubInventory.json");

            playerInventory.WriteInventory();
            hubInventory.WriteInventory();
            SceneManager.LoadScene("LD_Schema_01");
    }
}
