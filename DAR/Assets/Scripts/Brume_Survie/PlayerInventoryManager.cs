using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerInventoryManager : MonoBehaviour
{
    Sprite wood_Img;
    Sprite mushroom_Img;
    Sprite waterRation_Img;
    Sprite fish_Img;

    InteractWithItems.Inventory playerInventory;
    public GameObject inventoryUI;
    public GameObject pointerUI;
    
    string objectName;

    public GameObject bagInventoryPanel;
    public GameObject handInventoryPanel;
    public GameObject hubInventoryPanel;

    public Text _nb_Wood;
    public Text _nb_Mushroom;

    public GameObject[] inventorySlots;
    
    private bool m_isAxisInUse = false;

    void Start()
    { 
        wood_Img = Resources.Load<Sprite>("Images/wood_img"); 
        mushroom_Img = Resources.Load<Sprite>("Images/mushroom_img");
        waterRation_Img = Resources.Load<Sprite>("Images/water_img");
        fish_Img = Resources.Load<Sprite>("Images/fish_img");

        hubInventoryPanel = GameObject.Find("Inventory_Hub_Panel");
        _nb_Wood = GameObject.Find("nb_Wood").GetComponent<Text>();
        _nb_Mushroom = GameObject.Find("nb_Mushroom").GetComponent<Text>();
        hubInventoryPanel.SetActive(false);

        inventoryUI.SetActive(false);
        playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");
    }



    void Update()
    {

        if( Input.GetAxisRaw("UISelect") != 0)
        {
            if(m_isAxisInUse == false)
            {
                ShowUI();
                m_isAxisInUse = true;
            }
        }
        if( Input.GetAxisRaw("UISelect") == 0)
        {
            m_isAxisInUse = false;
        }    
    }

    public void AddItemInUI(string objectName)
    {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].GetComponent<Image>().sprite == null) {
                switch (objectName) {
                    case "wood":
                        inventorySlots[i].GetComponent<Image>().sprite = wood_Img;
                        inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 1f);
                        break;
                    case "fish":
                        inventorySlots[i].GetComponent<Image>().sprite = fish_Img;
                        inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 1f);
                        break;
                    case "waterRation":
                        inventorySlots[i].GetComponent<Image>().sprite = waterRation_Img;
                        inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 1f);
                        break;
                    case "mushroom":
                        inventorySlots[i].GetComponent<Image>().sprite = mushroom_Img;
                        inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 1f);
                        break;
                    
                    }
                    break;
                }
         }
    }

    public void ClearItemInUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].GetComponent<Image>().sprite = null;
            inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 0f);
        }
    }

    public void ChangeInventoryUI(InteractWithItems.Inventory _playerInventory)
    {
        ShowUI();
        handInventoryPanel.SetActive(false);
        bagInventoryPanel.SetActive(true);

        inventorySlots = new GameObject[_playerInventory.inventorySpace];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = GameObject.Find("bagInventorySlots").transform.GetChild(i).gameObject;
        }

        UpdateInventoryUI(_playerInventory);

    }

    public void UpdateInventoryUI(InteractWithItems.Inventory playerInventory)
    {
        ClearItemInUI();
        string[] inventoryTemp = playerInventory.listContent();
        foreach (string item in inventoryTemp) {
            AddItemInUI(item);
        }
    }

    public void ShowUI()
    {
        mouseLook despoMouseLook = GetComponentInChildren<mouseLook>();  
        if (inventoryUI.activeSelf) {
            Cursor.lockState = CursorLockMode.Locked;
            despoMouseLook.ignore = false;
            pointerUI.SetActive(true);
            inventoryUI.SetActive(false);
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            despoMouseLook.ignore = true;
            playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");
            pointerUI.SetActive(false);
            inventoryUI.SetActive(true);
        }
    }

    public void ShowUIHub()
    {
        mouseLook despoMouseLook = GetComponentInChildren<mouseLook>();
        if (hubInventoryPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            despoMouseLook.ignore = false;
            pointerUI.SetActive(true);
            inventoryUI.SetActive(false);
            handInventoryPanel.SetActive(true);
            hubInventoryPanel.SetActive(false);
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            despoMouseLook.ignore = true;
            pointerUI.SetActive(false);
            inventoryUI.SetActive(true);
            handInventoryPanel.SetActive(false);
            hubInventoryPanel.SetActive(true);
            InteractWithItems.Inventory hubInventory = InteractWithItems.Inventory.ReadInventory("HubInventory.json");
            _nb_Wood.text = "x " + hubInventory.wood;
            _nb_Mushroom.text = "x " + hubInventory.mushroom;
        }
    }


    void vide()
    {


        /*
public Text objMush_Count;
public Image objMush_Img;

public Text objWater_Count;
public Image objWater_Img;

public Text objFish_Count;
public Image objFish_Img;

public Sprite mush_Img;
public Sprite water_Img;
public Sprite fish_Img;

public Text objWood_Count;
public Image objWood_Img;
public Image objWood2_Img;

*/

        /* if (playerInventory.wood == 1 && playerInventory.inventorySpace != 0) {
         objWood_Img.gameObject.SetActive(true);
         objWood_Img.GetComponent<Image>().sprite = wood_Img;
        // objWood_Count.text = playerInventory.wood.ToString();
     }

     if (playerInventory.wood == 2 && playerInventory.inventorySpace != 0) {
         objWood_Img.gameObject.SetActive(true);
         objWood_Img.GetComponent<Image>().sprite = wood_Img;
         objWood2_Img.gameObject.SetActive(true);
         objWood2_Img.GetComponent<Image>().sprite = wood_Img;
         // objWood_Count.text = playerInventory.wood.ToString();
     }

     if (playerInventory.wood == 0) {
         objWood_Img.gameObject.SetActive(false);
         objWood2_Img.gameObject.SetActive(false);
     }

    */

        /* 
         * 
        * if (playerInventory.mushroom > 0)
         {
             objMush_Img.gameObject.SetActive(true);
             objMush_Img.GetComponent<Image>().sprite = mush_Img;
             objMush_Count.text = playerInventory.mushroom.ToString();
         }
         else
         {
             objMush_Img.gameObject.SetActive(false);
             objMush_Img.GetComponent<Image>().sprite = mush_Img;
             objMush_Count.text = playerInventory.mushroom.ToString();
         } */



        /* if (playerInventory.waterRation > 0)
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

    public void AssignSprite()
    {
        mush_Img = Resources.Load("Assets/Ressources/Images/inventaire.png") as Sprite;
        wood_Img = Resources.Load("Assets/Ressources/Images/wood_img") as Sprite;
        water_Img = Resources.Load("Assets/Ressources/Images/water_img") as Sprite;
        fish_Img = Resources.Load("Assets/Ressources/Images/fish_img") as Sprite;
    }
        */
    }

}











