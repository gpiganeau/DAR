using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InteractWithItems : MonoBehaviour
{
    Vector2 center;
    float x;
    float y;
    string objectName;
    public Camera player_camera;
    Inventory playerInventory;
    Inventory hubInventory;
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
        hubInventory = Inventory.ReadInventory("HubInventory.json");
        woodStorage.GetComponent<woodStorage>().Show(hubInventory.wood);
        infoManager = GetComponent<InfoManager>();
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
                    infoManager.ShowInfo(objectName + " added");
                }
                
                break;
            
            case "wood":      
                if (playerInventory.usedInventorySpace != playerInventory.inventorySpace) {   
                    playerInventory.wood += 1;
                    playerInventory.usedInventorySpace += 1;
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    infoManager.ShowInfo(objectName + " added");
                } 
                break;

            case "fish":
                playerInventory.fish += 1;
                infoManager.ShowInfo(objectName + " added");
                break;
            case "waterRation":
                playerInventory.waterRation += 1;
                infoManager.ShowInfo(objectName + " added");
                break;
            case "chest":
                DepositInventory();
                collectible.GetComponent<ChestScript>().Open();
                gameObject.GetComponent<PlayerInventoryManager>().ClearItemInUI();
                break;
            case "bed":
                if (gameObject.GetComponent<PlayerStatus>().GetShelteredStatus() && gameObject.GetComponent<PlayerStatus>().GetWarmStatus()) {
                    gameObject.GetComponent<EndDay>().EndThisDayInside();
                    infoManager.ShowInfo("Day completed !");
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
                infoManager.ShowInfo(objectName + " acquired");
                break;
            case "fireplace":
                playerInventory = Inventory.ReadInventory("PlayerInventory.json");
                hubInventory = Inventory.ReadInventory("HubInventory.json");
                if (playerInventory.wood + hubInventory.wood >= 3) {
                    playerInventory = collectible.GetComponent<FireplaceScript>().Light();
                    infoManager.ShowInfo("Fire lit !");
                }
                hubInventory = Inventory.ReadInventory("HubInventory.json");

                woodStorage.GetComponent<woodStorage>().Show(hubInventory.wood);
                break;

            case "woodStorage":
                    DepositWood();
                    break;

        }

        playerInventory.WriteInventory();
		gameObject.GetComponent<PlayerInventoryManager>().UpdateInventoryUI(playerInventory);
        return;
    }

    void DepositWood()
    {
        hubInventory.wood += playerInventory.wood;
        playerInventory.wood = 0;
        hubInventory.WriteInventory();
        woodStorage.GetComponent<woodStorage>().Show(hubInventory.wood);
        playerInventory.WriteInventory();
        
    }

    void DepositInventory() {
        playerInventory.DepositInInventory(hubInventory);
        playerInventory = new Inventory(playerInventory.inventorySpace, "PlayerInventory.json");
        playerInventory.usedInventorySpace = 0;
        playerInventory.WriteInventory();
        woodStorage.GetComponent<woodStorage>().Show(hubInventory.wood);
    }

    public void LoseInventory() {
        playerInventory = new Inventory(playerInventory.inventorySpace, "PlayerInventory.json");
        playerInventory.WriteInventory();
        
    }

    public void ConsumeRessources() {
        playerInventory.ConsumeDayRessources(hubInventory);
    }

    public Inventory ChangeInventory(int _newInventorySpace)
    {
        playerInventory.oldUsedInventorySpace = playerInventory.usedInventorySpace;
        playerInventory.inventorySpace = _newInventorySpace;
        playerInventory.usedInventorySpace = playerInventory.oldUsedInventorySpace;
        gameObject.GetComponent<PlayerInventoryManager>().ChangeInventoryUI(playerInventory);
        return playerInventory;

    }

    public class Inventory {
        public int mushroom;
        public int fish;
        public int wood;
        public int waterRation;
        public int inventorySpace;
        public int usedInventorySpace;
        public int oldUsedInventorySpace;
        public string filename;


        public Inventory(int newInventorySpace, string _filename) {
            mushroom = 0;
            fish = 0;
            wood = 0;
            waterRation = 0;
            inventorySpace = newInventorySpace;
            usedInventorySpace = 0;
            filename = _filename;
        }


        public static Inventory ReadHubInventory()
        {
            Inventory hubInventory;
            string HubInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json");
            hubInventory = JsonUtility.FromJson<Inventory>(HubInventoryJSON);
            return hubInventory;
        }

        public static void WriteHubInventory(Inventory newHubInventory)
        {
            //GameObject.Find("woodStorage").GetComponent<woodStorage>().Show(newHubInventory.wood);
            string uploadInventory = JsonUtility.ToJson(newHubInventory);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json", uploadInventory);
        }

        public static Inventory ReadInventory(string filename) {
            string InventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename);
            Inventory returnInventory = JsonUtility.FromJson<Inventory>(InventoryJSON);
            return returnInventory;
        }

        public void WriteInventory() {
            string uploadInventory = JsonUtility.ToJson(this);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename, uploadInventory);
        }
        

        public void DepositInInventory(Inventory target) {
            target.mushroom += mushroom;
            target.waterRation += waterRation;
            target.fish += fish;
            target.wood += wood;

            target.WriteInventory();
            
        }

        public void ConsumeDayRessources(Inventory target) {
            target.waterRation -= 2;
            int amountToEat = 5;
            while (amountToEat > 0) {
                if (target.fish > 0) {
                    target.fish -= 1;
                    amountToEat -= 2;
                }
                else if (target.mushroom > 0) {
                    target.mushroom -= 1;
                    amountToEat -= 1;
                }
                else {
                    amountToEat = 0;
                }
                
            }

            target.wood = Mathf.Clamp(target.wood, 0, 99);
            target.waterRation = Mathf.Clamp(target.wood, 0, 99);
            target.mushroom = Mathf.Clamp(target.wood, 0, 99);
            target.fish = Mathf.Clamp(target.fish, 0, 99);

            target.WriteInventory();

        }

        public string[] listContent() 
        {
            int counter = 0;
            string[] returnList = new string[usedInventorySpace];
            for (int i = 0; i < wood; i++)
            {
                returnList[counter] = "wood";
                counter++;
            }
            for (int i = 0; i < fish; i++)
            {
                returnList[counter] = "fish";
                counter++;
            }
            for (int i = 0; i < mushroom; i++)
            {
                returnList[counter] = "mushroom";
                counter++;
            }
            for (int i = 0; i < waterRation; i++)
            {
                returnList[counter] = "waterRation";
                counter++;
            }

            return returnList;
        }
            

    }


}
