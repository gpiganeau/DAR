using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InteractWithItems : MonoBehaviour
{
    public Text mTextName;
    public Text mTextInteraction;
    public GameObject mText;

    public HubInventoryManager hubInventoryManager;
    public PlayerInventoryManager playerInventoryManager;
    [SerializeField] private taskManager taskManagerObject;

    Vector2 center;
    float x;
    float y;

    [SerializeField] Item wood;
    [SerializeField] Item balise;
    [SerializeField] Item mushroom;
    [SerializeField] Item plank;
    [SerializeField] Item radioPiece1;
    [SerializeField] Item radioPiece2;
    [SerializeField] Item radioPiece3;
    [SerializeField] Item radioPiece4;
    [SerializeField] Item baie;
    [SerializeField] Item poissonCru;
    [SerializeField] Item poissonCuit;

    string objectName;
    public Camera player_camera;
    //Inventory hubInventory;
    private InfoManager infoManager;
    private bool m_isAxisInUse = false;
    GameObject woodStorage;


    public Animator anim;
    // Start is called before the first frame update

    public void Awake()
    {
       woodStorage = GameObject.Find("woodStorage");
    }

    void Start()
    {
        mText.SetActive(false);
        x = Screen.width / 2;
        y = Screen.height / 2;
        infoManager = gameObject.GetComponent<InfoManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(GameManager.GM.interaction)) {
            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
            if (Physics.Raycast(ray, out hit, 5, ~(1 << 11))) {
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.tag == "interactible" || objectHit.tag == "Food") {
                    Item collectible = objectHit.gameObject.GetComponent<ItemInteraction>().GetCollectible();
                    if (collectible != null) {
                        Collect(collectible, objectHit);
                    }
                    else {
                        objectName = objectHit.gameObject.GetComponent<ItemInteraction>().GetName();
                        Action(objectName, objectHit);
                    }
                }
            }
        }

        else if (Input.GetAxisRaw("Interact") != 0)
        {
            if(m_isAxisInUse == false)
            {
                RaycastHit hit;
                Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
                if (Physics.Raycast(ray, out hit, 5, ~(1 << 11)))
                {
                    GameObject objectHit = hit.collider.gameObject;
                    if (objectHit.tag == "interactible" || objectHit.tag == "Food")
                    {
                        Item collectible = objectHit.gameObject.GetComponent<ItemInteraction>().GetCollectible();
                        if (collectible != null) {
                            Collect(collectible, objectHit);
                        }
                        else {
                            objectName = objectHit.gameObject.GetComponent<ItemInteraction>().GetName();
                            Action(objectName, objectHit);
                        }
                    }
                }
                m_isAxisInUse = true;
            }
        }
        
        else if( Input.GetAxisRaw("Interact") == 0)
        {
            //mText.SetActive(false);
            m_isAxisInUse = false;
        }
    }

    void Collect(Item collectible, GameObject objectHit) {
        if (playerInventoryManager.AddItem(Instantiate(collectible))) {
            objectHit.GetComponent<ItemInteraction>().RemoveOneUse();
            string display = collectible._name + " collecté";
            CheckForTasks(collectible);
            infoManager.ShowInfo(display);
        }
        else {
            infoManager.ShowInfo("Inventaire plein");
        }
    }

    void Action(string objectName, GameObject collectible) {
        
        switch (objectName){

            case "Coffre":
                //playerInventoryManager.DepositInventory();
                //collectible.GetComponent<ChestScript>().Open();
                gameObject.GetComponent<PlayerInventoryManager>().ShowAlternateUI(2);
                break;
            case "Lit":
                if (gameObject.GetComponent<PlayerStatus>().GetShelteredStatus() && gameObject.GetComponent<PlayerStatus>().GetWarmStatus()) {
                    gameObject.GetComponent<EndDay>().EndThisDayInside();
                    taskManagerObject.CompleteTask("GTS");
                    infoManager.ShowInfo("Journée finie !");
                }
                break;
            case "Porte":
                if (!gameObject.GetComponent<PlayerStatus>().GetIsRestingStatus()) {
                    collectible.GetComponent<DoorScript>().Open();
                }
                
                break;
            case "Sac à dos":
                playerInventoryManager.ChangeInventory(9);
                collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                taskManagerObject.CompleteTask("FTB");
                infoManager.ShowInfo("Vous avez trouvé le sac");
                break;
            case "Poêle":
                if (playerInventoryManager.GetInventory().CountItem("Bûche") + hubInventoryManager.GetHubInventory().Count("Bûche") >= 3) {
                    collectible.GetComponent<FireplaceScript>().Light();
                    taskManagerObject.CompleteTask("LTF");
                    taskManagerObject.AddTask("GTS");
                    infoManager.ShowInfo("Feu allumé !");
                }
                break;

            case "Pile de bois":
                playerInventoryManager.DepositInventory("Bûche");
                break;
            case "Établi":
                gameObject.GetComponent<PlayerInventoryManager>().ShowAlternateUI(3);
                break;

            case "poissonCru":
                Item newItem8 = Instantiate<Item>(poissonCru);
                if (playerInventoryManager.AddItem(newItem8)) {
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Poisson ajouté");
                }
                else {
                    infoManager.ShowInfo("Nécéssite " + poissonCru.weight + " places !");
                }
                break;
            case "poissonCuit":
                Item newItem9 = Instantiate<Item>(poissonCuit);
                if (playerInventoryManager.AddItem(newItem9)) {
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Poisson cuit ajouté");
                }
                else {
                    infoManager.ShowInfo("Nécéssite " + poissonCuit.weight + " places !");
                }
                break;
            case "Pêcher":
                collectible.GetComponent<FishingScript>().StartFishing();
                break;
            case "Zone de pêche" :
                collectible.GetComponent<NewPosition>().isFishing = true;
                break;
            case "Balise":
                Item newItem10 = Instantiate<Item>(balise);
                if (playerInventoryManager.AddItem(newItem10))
                {
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Balise ajoutée");
                }
                break;
        }
        return;
    }

    void CheckForTasks(Item collectible) //Activation des tâches liées à la collecte d'objet
    {   
        switch (collectible._name) {
            case "Bûche":
                if (taskManagerObject.FindTask("GSW") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GSW");
                    if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GSW");
                        taskManagerObject.AddTask("LTF");
                    }
                }
                break;
            case "Baie":
                if (taskManagerObject.FindTask("GSF") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GSF");
                    if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GSF");
                    }
                }
                break;
            case "Antenne":
                if (taskManagerObject.FindTask("GSH") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GSH");
                    if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GSH");
                        taskManagerObject.AddTask("GRP");
                    }
                }
                else if (taskManagerObject.FindTask("GRP") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GRP");
                    // Il manque la condition "avoir les 4 pieces radio"...
                    /*if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GRP");
                    }*/
                }
                break;
            case "Générateur":
                if (taskManagerObject.FindTask("GSH") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GSH");
                    if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GSH");
                        taskManagerObject.AddTask("GRP");
                    }
                }
                else if (taskManagerObject.FindTask("GRP") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GRP");
                    // Il manque la condition "avoir les 4 pieces radio"...
                    /*if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GRP");
                    }*/
                }
                break;
            case "Haut-Parleur":
                if (taskManagerObject.FindTask("GSH") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GSH");
                    if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GSH");
                        taskManagerObject.AddTask("GRP");
                    }
                }
                else if (taskManagerObject.FindTask("GRP") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GRP");
                    // Il manque la condition "avoir les 4 pieces radio"...
                    /*if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GRP");
                    }*/
                }
                break;
            case "Potentiomètre":
                if (taskManagerObject.FindTask("GSH") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GSH");
                    if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GSH");
                        taskManagerObject.AddTask("GRP");
                    }
                }
                else if (taskManagerObject.FindTask("GRP") != null)
                {
                    Tasks woodTask = taskManagerObject.FindTask("GRP");
                    // Il manque la condition "avoir les 4 pieces radio"...
                    /*if (woodTask._isCompleted == false)
                    {
                        taskManagerObject.CompleteTask("GRP");
                    }*/
                }
                break;
        }
    }

    public Item GetItem(string itemName) {
        if (itemName == wood._name) {
            return wood;
        }
        else if (itemName == plank._name) {
            return plank;
        }
        else if (itemName == mushroom._name) {
            return mushroom;
        }
        return new Item();
    }

    
    
}
