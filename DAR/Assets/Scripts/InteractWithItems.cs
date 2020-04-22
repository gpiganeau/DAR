using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InteractWithItems : MonoBehaviour
{
    public HubInventoryManager hubInventoryManager;
    public PlayerInventoryManager playerInventoryManager;

    Vector2 center;
    float x;
    float y;

    [SerializeField] Item wood;
    [SerializeField] Item mushroom;
    [SerializeField] Item plank;
    [SerializeField] Item radioPiece1;
    [SerializeField] Item radioPiece2;
    [SerializeField] Item radioPiece3;
    [SerializeField] Item radioPiece4;

    string objectName;
    public Camera player_camera;
    //Inventory hubInventory;
    private InfoManager infoManager;
    private bool m_isAxisInUse = false;
    GameObject woodStorage;
    // Start is called before the first frame update

    public void Awake()
    {
       woodStorage = GameObject.Find("woodStorage");
    }

    void Start()
    {
        
        x = Screen.width / 2;
        y = Screen.height / 2;
        infoManager = gameObject.GetComponent<InfoManager>();
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("Interact") != 0)
        {
            if(m_isAxisInUse == false)
            {
                RaycastHit hit;
                Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
                if (Physics.Raycast(ray, out hit, 5, ~(1 << 11)))
                {
                    GameObject objectHit = hit.collider.gameObject;
                    if (objectHit.tag == "interactible")
                    {
                        objectName = objectHit.gameObject.GetComponent<ItemInteraction>().GetName();
                        Action(objectName, objectHit);
                    }
                }
                m_isAxisInUse = true;
            }
        }
        
        if( Input.GetAxisRaw("Interact") == 0)
        {
            m_isAxisInUse = false;
        }
    }

   

    void Action(string objectName, GameObject collectible) {
        
        switch (objectName){

            case "mushroom":
                Item newItem = Instantiate<Item>(mushroom);
                if (playerInventoryManager.AddItem(newItem)) {
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Champignon ajouté");
                }
                else {
                    infoManager.ShowInfo("Inventaire plein");
                }
                break;
            
            case "wood":
                Item newItem1 = Instantiate<Item>(wood);
                if (playerInventoryManager.AddItem(newItem1)) {
                   
                    
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Bois ajouté");
                }
                else {
                    infoManager.ShowInfo("Inventaire plein");
                }
                break;
            case "plank":
                Item newItem2 = Instantiate<Item>(plank);
                if (playerInventoryManager.AddItem(newItem2)){
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Planche ajouté");
                }
                else
                {
                    infoManager.ShowInfo("Inventaire plein");
                }
                break;
            /* case "wood":      
                            if (playerInventoryManager.GetInventory().maxWeight != playerInventoryManager.GetInventory().currentWeight) {
                                Item newItem = Instantiate<Item>(wood);
                                playerInventoryManager.AddItem(newItem);
                                collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                                infoManager.ShowInfo("Bois ajouté");
                            }
                            else {
                                infoManager.ShowInfo("Inventaire plein");
                            }
                            break;
                        case "fish":
                            playerInventory.fish += 1;
                            infoManager.ShowInfo("Poisson ajouté");
                            break;
                        case "waterRation":
                            playerInventory.waterRation += 1;
                            infoManager.ShowInfo("Ration d'eau ajoutée");
                            break;
            */
            case "chest":
                //playerInventoryManager.DepositInventory();
                collectible.GetComponent<ChestScript>().Open();
                gameObject.GetComponent<PlayerInventoryManager>().ShowAlternateUI(2);
                break;
            case "bed":
                if (gameObject.GetComponent<PlayerStatus>().GetShelteredStatus() && gameObject.GetComponent<PlayerStatus>().GetWarmStatus()) {
                    gameObject.GetComponent<EndDay>().EndThisDayInside();
                    infoManager.ShowInfo("Journée finie !");
                }
                break;
            case "door":
                if (!gameObject.GetComponent<PlayerStatus>().GetIsRestingStatus()) {
                    collectible.GetComponent<DoorScript>().Open();
                }
                
                break;
            case "bag":
                playerInventoryManager.ChangeInventory(9);
                collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                infoManager.ShowInfo("Vous avez trouvé le sac");
                break;
            case "fireplace":
                if (playerInventoryManager.GetInventory().CountItem("wood") + hubInventoryManager.GetHubInventory().Count("wood") >= 3) {
                    collectible.GetComponent<FireplaceScript>().Light();
                    infoManager.ShowInfo("Feu allumé !");
                }
                break;

            case "woodStorage":
                playerInventoryManager.DepositInventory("wood");
                break;
            case "workTable":
                gameObject.GetComponent<PlayerInventoryManager>().ShowAlternateUI(3);
                break;

            case "radioPiece1":
                Item newItem3 = Instantiate<Item>(radioPiece1);
                if (playerInventoryManager.AddItem(newItem3)) {
                   
                    
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Pièce radio ajoutée");
                }
                else {
                    infoManager.ShowInfo("Nécéssite " + radioPiece1.weight + " places !");
                }
                break;
            case "radioPiece2":
                Item newItem4 = Instantiate<Item>(radioPiece2);
                if (playerInventoryManager.AddItem(newItem4)) {
                   
                    
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Pièce radio ajoutée");
                }
                else {
                    infoManager.ShowInfo("Nécéssite " + radioPiece2.weight + " places !");
                }
                break;
            case "radioPiece3":
                Item newItem5 = Instantiate<Item>(radioPiece3);
                if (playerInventoryManager.AddItem(newItem5)) {
                   
                    
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Pièce radio ajoutée");
                }
                else {
                    infoManager.ShowInfo("Nécéssite " + radioPiece3.weight + " places !");
                }
                break;
            case "radioPiece4":
                Item newItem6 = Instantiate<Item>(radioPiece4);
                if (playerInventoryManager.AddItem(newItem6)) {
                   
                    
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Pièce radio ajoutée");
                }
                else {
                    infoManager.ShowInfo("Nécéssite " + radioPiece4.weight + " places !");
                }
                break;
        }
        return;
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
