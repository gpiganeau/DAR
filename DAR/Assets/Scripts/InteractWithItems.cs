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
    // Start is called before the first frame update
    void Start()
    {
        
        x = Screen.width / 2;
        y = Screen.height / 2;
       
        
        string playerInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json");
        string HubInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json");
        playerInventory = JsonUtility.FromJson<Inventory>(playerInventoryJSON);
        hubInventory = JsonUtility.FromJson<Inventory>(HubInventoryJSON);
        
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
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
        }
    }

   

    void Action(string objectName, GameObject collectible) {
        
        switch (objectName){
            case "mushroom":
                if (playerInventory.usedInventorySpace !=  playerInventory.inventorySpace)
                {
                    playerInventory.mushroom += 1;
                    playerInventory.usedInventorySpace += 1;
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    gameObject.GetComponent<PlayerInventoryManager>().AddItemInUI(objectName);

                }
                
                break;
            
            case "wood":      
                if (playerInventory.usedInventorySpace != playerInventory.inventorySpace) {   

                    playerInventory.wood += 1;
                    playerInventory.usedInventorySpace += 1;
                    collectible.GetComponent<ItemInteraction>().RemoveOneUse();
                    gameObject.GetComponent<PlayerInventoryManager>().AddItemInUI(objectName);   
                } 
                break;

            case "fish":
                playerInventory.fish += 1;
                break;
            case "waterRation":
                playerInventory.waterRation += 1;
                break;
            case "chest":
                DepositInventory();
                collectible.GetComponent<ChestScript>().Open();
                gameObject.GetComponent<PlayerInventoryManager>().ClearItemInUI();
                break;
            case "bed":
                if (gameObject.GetComponent<PlayerStatus>().GetShelteredStatus() && gameObject.GetComponent<PlayerStatus>().GetWarmStatus()) {
                    gameObject.GetComponent<EndDay>().EndThisDayInside();
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
                
                break;
            case "fireplace":
                string playerInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json");
                string HubInventoryJSON = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/HubInventory.json");
                playerInventory = JsonUtility.FromJson<Inventory>(playerInventoryJSON);
                hubInventory = JsonUtility.FromJson<Inventory>(HubInventoryJSON);
                if (playerInventory.wood + hubInventory.wood >= 3) {
                    playerInventory = collectible.GetComponent<FireplaceScript>().Light();
                }
                
                break;
        }

        string uploadInventory = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json", uploadInventory);

        return;
    }

    void DepositInventory() {
        playerInventory.DepositInInventory(hubInventory, "HubInventory.json");
        playerInventory = new Inventory(playerInventory.inventorySpace);
        playerInventory.usedInventorySpace = 0;
        string uploadInventory = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json", uploadInventory);
    }

    public void LoseInventory() {
        playerInventory = new Inventory(playerInventory.inventorySpace);
        string uploadInventory = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/PlayerInventory.json", uploadInventory);
    }

    public void ConsumeRessources() {
        playerInventory.ConsumeDayRessources(hubInventory, "HubInventory.json");
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



        public Inventory(int newInventorySpace) {
            mushroom = 0;
            fish = 0;
            wood = 0;
            waterRation = 0;
            inventorySpace = newInventorySpace;
            usedInventorySpace = 0;

        }
        

        public void DepositInInventory(Inventory target, string filename) {
            target.mushroom += mushroom;
            target.waterRation += waterRation;
            target.fish += fish;
            target.wood += wood;

            string uploadInventory = JsonUtility.ToJson(target);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename, uploadInventory);
            
        }

        public void ConsumeDayRessources(Inventory target, string filename) {
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

            string uploadInventory = JsonUtility.ToJson(target);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/" + filename, uploadInventory);

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
