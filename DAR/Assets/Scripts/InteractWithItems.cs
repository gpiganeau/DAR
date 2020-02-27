using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InteractWithItems : MonoBehaviour
{
    Vector2 center;
    float x;
    float y;
    string objectName;
    public Camera player_camera;
    Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        x = Screen.width / 2;
        y = Screen.height / 2;

        string playerInventoryJSON = File.ReadAllText(Application.dataPath + "/JSONFiles/CurrentDay.json");
        playerInventory = JsonUtility.FromJson<Inventory>(playerInventoryJSON);
    }



    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
            if (Physics.Raycast(ray, out hit, 10, ~(1 << 11))) {
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.tag == "interactible") {
                    objectName = objectHit.gameObject.GetComponent<ItemInteraction>().GetName();
                    Action(objectName);
                }
            }
        }
    }

    void Action(string objectName) {
        Debug.Log("Touched an item");
        switch (objectName){
            case "mushroom":
                playerInventory.mushroom += 1;
                break;
            case "fish":
                playerInventory.fish += 1;
                break;
            case "wood":
                playerInventory.wood += 1;
                break;
            case "waterRation":
                playerInventory.waterRation += 1;
                break;
            case "chest":
                DepositInventory();
                break;
        }

        string uploadInventory = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(Application.dataPath + "/JSONFiles/PlayerInventory.json", uploadInventory);

        return;
    }

    private class Inventory {
        public int mushroom;
        public int fish;
        public int wood;
        public int waterRation;
        
        public Inventory() {
            mushroom = 0;
            fish = 0;
            wood = 0;
            waterRation = 0;
        }
    }

}
