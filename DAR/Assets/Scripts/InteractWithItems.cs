using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InteractWithItems : MonoBehaviour
{
    public HubInventoryManager hubInventoryManager;
    Vector2 center;
    float x;
    float y;
    string objectName;
    public Camera player_camera;
    Inventory playerInventory;
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

        playerInventory = Inventory.ReadInventory("PlayerInventory.json");
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
                if (playerInventory.usedInventorySpace !=  playerInventory.inventorySpace) {
                    playerInventory.mushroom += 1;
                    playerInventory.usedInventorySpace += 1;
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo("Champignon ajouté");
                }
                else {
                    infoManager.ShowInfo("Inventaire plein");
                }
                break;
            
            case "wood":      
                if (playerInventory.usedInventorySpace != playerInventory.inventorySpace) {   
                    playerInventory.wood += 1;
                    playerInventory.usedInventorySpace += 1;
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
            case "chest":
                DepositInventory();
                collectible.GetComponent<ChestScript>().Open();
                gameObject.GetComponent<PlayerInventoryManager>().ShowAlternateUI(2);
                gameObject.GetComponent<PlayerInventoryManager>().ClearItemInUI();
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
                playerInventory = ChangeInventory(9);
                collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                infoManager.ShowInfo("Vous avez trouvé le sac");
                break;
            case "fireplace":
                playerInventory = Inventory.ReadInventory("PlayerInventory.json");
                if (playerInventory.wood + hubInventoryManager.GetHubInventory().wood >= 3) {
                    playerInventory = collectible.GetComponent<FireplaceScript>().Light();
                    infoManager.ShowInfo("Feu allumé !");
                }
                
                break;

            case "woodStorage":
                DepositWood();
                break;
            case "workTable":
                gameObject.GetComponent<PlayerInventoryManager>().ShowAlternateUI(3);
                break;

        }

        playerInventory.WriteInventory();
		gameObject.GetComponent<PlayerInventoryManager>().UpdateInventoryUI(playerInventory);
        return;
    }

    void DepositWood()
    {
        hubInventoryManager.ChangeValue("wood", playerInventory.wood);
        playerInventory.wood = 0;
        playerInventory.WriteInventory();
    }

    void DepositInventory() {
        playerInventory.DepositInInventory(hubInventoryManager);
        playerInventory = new Inventory(playerInventory.inventorySpace, "PlayerInventory.json");
        playerInventory.usedInventorySpace = 0;
        playerInventory.WriteInventory();
    }

    public void LoseInventory() {
        playerInventory = new Inventory(playerInventory.inventorySpace, "PlayerInventory.json");
        playerInventory.WriteInventory();    }

    public void ConsumeRessources() {
        playerInventory.ConsumeDayRessources(hubInventoryManager);
    }

    public Inventory UseRessources(string ressourceName, int amount) {
        playerInventory = Inventory.ReadInventory("PlayerInventory.json");
        switch (ressourceName) {
            case "wood":
                while (playerInventory.wood > 0 && amount > 0) {
                    playerInventory.wood -= 1;
                    amount -= 1;
                }
                hubInventoryManager.ChangeValue("wood", -amount);
                playerInventory.WriteInventory();
                break;

            case "mushroom":
                break;

            case "plank":
                break;
        }
        playerInventory.WriteInventory();
        return playerInventory;
    }


    public Inventory ChangeInventory(int _newInventorySpace)
    {
        playerInventory.oldUsedInventorySpace = playerInventory.usedInventorySpace;
        playerInventory.inventorySpace = _newInventorySpace;
        playerInventory.usedInventorySpace = playerInventory.oldUsedInventorySpace;
        gameObject.GetComponent<PlayerInventoryManager>().ChangeInventoryUI(playerInventory);
        return playerInventory;

    }

    
    
}
