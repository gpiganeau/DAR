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
    public GameObject pointerUI;
    
    string objectName;

    public GameObject[] UIElements = new GameObject[3];
    private GameObject currentInventoryPanel;


    public GameObject[] inventorySlots;
    
    private bool m_isAxisInUse = false;

    void Start()
    { 
        wood_Img = Resources.Load<Sprite>("Images/wood_img"); 
        mushroom_Img = Resources.Load<Sprite>("Images/mushroom_img");
        waterRation_Img = Resources.Load<Sprite>("Images/water_img");
        fish_Img = Resources.Load<Sprite>("Images/fish_img");

        UIElements[2] = GameObject.Find("Inventory_Hub_Panel");

        playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");

        currentInventoryPanel = UIElements[0];

        foreach (GameObject UIElement in UIElements) {
            UIElement.SetActive(false);
        }
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
            if (inventorySlots[i].GetComponent<Image>().sprite == null)
            {
                switch (objectName)
                {
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
        GameObject temp = UIElements[0];
        UIElements[0] = UIElements[1];
        UIElements[1] = temp;
        ShowUI();
        currentInventoryPanel.SetActive(false); //On éteind l'ancien panel
        currentInventoryPanel = UIElements[0];  //On change le current panel
        currentInventoryPanel.SetActive(true);  // Et on le rallume

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
        if (currentInventoryPanel.activeSelf) {
            Cursor.lockState = CursorLockMode.Locked;
            despoMouseLook.ignore = false;
            pointerUI.SetActive(true);
            currentInventoryPanel.SetActive(false);
            currentInventoryPanel = UIElements[0];
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            despoMouseLook.ignore = true;
            playerInventory = InteractWithItems.Inventory.ReadInventory("PlayerInventory.json");
            pointerUI.SetActive(false);
            currentInventoryPanel.SetActive(true);
        }
    }

    public void ShowAlternateUI(int UIIndex) {
        if (!currentInventoryPanel.activeSelf) {
            currentInventoryPanel = UIElements[UIIndex];
            ShowUI();
        }
        else {
            ShowUI();
        }
    }
}











