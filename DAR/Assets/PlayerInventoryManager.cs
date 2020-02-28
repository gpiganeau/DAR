using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerInventoryManager : MonoBehaviour
{
    public Text objMush_Count;
    public Image objMush_Img;

    public Text objWood_Count;
    public Image objWood_Img;

    public Text objWater_Count;
    public Image objWater_Img;

    public Text objFish_Count;
    public Image objFish_Img;

    public Sprite mush_Img;
    public Sprite water_Img;
    public Sprite wood_Img;
    public Sprite fish_Img;

    InteractWithItems.Inventory playerInventory;
    public GameObject inventoryUI;
    public GameObject pointerUI;
    string objectName;


    void Start()
    {
        string playerInventoryJSON = File.ReadAllText(Application.dataPath + "/JSONFiles/PlayerInventory.json");
        playerInventory = JsonUtility.FromJson<InteractWithItems.Inventory>(playerInventoryJSON);
    }



    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && inventoryUI.activeSelf == false)
        {
            string playerInventoryJSON = File.ReadAllText(Application.dataPath + "/JSONFiles/PlayerInventory.json");
            playerInventory = JsonUtility.FromJson<InteractWithItems.Inventory>(playerInventoryJSON);
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
            objMush_Img.gameObject.SetActive(true);
            objMush_Img.GetComponent<Image>().sprite = mush_Img;
            objMush_Count.text = playerInventory.mushroom.ToString();
        }
        else
        {
            objMush_Img.gameObject.SetActive(false);
            objMush_Count.text = playerInventory.mushroom.ToString();
        }

        if (playerInventory.wood > 0)
        {
            objWood_Img.gameObject.SetActive(true);
            objWood_Img.GetComponent<Image>().sprite = wood_Img;
            objWood_Count.text = playerInventory.wood.ToString();
        }
        else
        {
            objWood_Img.gameObject.SetActive(false);
            objWood_Count.text = playerInventory.wood.ToString();
        }

        if (playerInventory.waterRation > 0)
        {
            objWater_Img.gameObject.SetActive(true);
            objWater_Img.GetComponent<Image>().sprite = water_Img;
            objWater_Count.text = playerInventory.waterRation.ToString();
        }
        else
        {
            objWater_Img.gameObject.SetActive(false);
            objWater_Count.text = playerInventory.waterRation.ToString();
        }

        if (playerInventory.fish > 0)
        {
            objFish_Img.gameObject.SetActive(true);
            objFish_Img.GetComponent<Image>().sprite = fish_Img;
            objFish_Count.text = playerInventory.fish.ToString();
        }

        else
        {
            objFish_Img.gameObject.SetActive(false);
            objFish_Count.text = playerInventory.fish.ToString();
        }

    }
}
