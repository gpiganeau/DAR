using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InteractWithItems : MonoBehaviour
{
    public Text obj_Count;
    public Image obj_Img;
    public Sprite mush_Img;

    Vector2 center;
    float x;
    float y;
    string objectName;
    public Camera player_camera;
    Inventory playerInventory;
    public GameObject inventoryUI;
    public GameObject pointerUI;
    // Start is called before the first frame update
    void Start()
    {

        x = Screen.width / 2;
        y = Screen.height / 2;

        string playerInventoryJSON = File.ReadAllText(Application.dataPath + "/JSONFiles/CurrentDay.json");
        playerInventory = JsonUtility.FromJson<Inventory>(playerInventoryJSON);
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

        if (Input.GetKeyDown(KeyCode.Tab) && inventoryUI.activeSelf == false )
        {

            pointerUI.SetActive(false);
            inventoryUI.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Tab) && inventoryUI.activeSelf == true)
        {
            pointerUI.SetActive(true);
            inventoryUI.SetActive(false);
        }

        if (playerInventory.mushroom > 0)
        {
            obj_Img.gameObject.SetActive(true);
            obj_Img.GetComponent<Image>().sprite = mush_Img; 
            obj_Count.text = playerInventory.mushroom.ToString();
        }
        

    }

    private void DepositInventory()
    {
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

public class Inventory
{

    public int mushroom;
    public int fish;
    public int wood;
    public int waterRation;

    public Inventory()
    {
        mushroom = 0;
        fish = 0;
        wood = 0;
        waterRation = 0;
    }




    
}





