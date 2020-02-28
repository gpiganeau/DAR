using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InteractWithItems : MonoBehaviour
{
   /* public Text obj_Count;
    public Image obj_Img;
    public Sprite mush_Img; */ 

    Vector2 center;
    float x;
    float y;
    string objectName;
    public Camera player_camera;
    PlayerInventory playerInventory;
    //public GameObject inventoryUI;
    //public GameObject pointerUI;
    // Start is called before the first frame update
    void Start()
    {

        x = Screen.width / 2;
        y = Screen.height / 2;

        string playerInventoryJSON = File.ReadAllText(Application.dataPath + "/JSONFiles/PlayerInventory.json");
        playerInventory = JsonUtility.FromJson<PlayerInventory>(playerInventoryJSON);
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
            if (Physics.Raycast(ray, out hit, 10, ~(1 << 11)))
            {
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.tag == "interactible")
                {
                    objectName = objectHit.gameObject.GetComponent<ItemInteraction>().GetName();
                    Action(objectName);
                }
            }
        }
    }

    private void DepositInventory()
    {
    }

    public class PlayerInventory
    {

        public int mushroom;
        public int fish;
        public int wood;
        public int waterRation;

        PlayerInventory()
        {
            mushroom = 0;
            fish = 0;
            wood = 0;
            waterRation = 0;
        }

    }
    void Action(string objectName)
        {
            Debug.Log("Touched an item");
            switch (objectName)
            {
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
    }







