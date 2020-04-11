using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerInventoryManager : MonoBehaviour
{

    Inventory playerInventory;
    

    [SerializeField] private HubInventoryManager hubInventoryManager;
    public GameObject pointerUI;

    public GameObject[] UIElements = new GameObject[4];
    private GameObject currentInventoryPanel;

    public GameObject[] inventorySlots;
    
    private bool m_isAxisInUse = false;

    void Start()
    {
        //Set un nouvel inventaire ou lit les données d'inventaire sauvegardées
        if (HubInventoryManager.initializeWithLoad) {
            playerInventory = new Inventory(2, "PlayerInventory.json");
        }
        else {
            playerInventory = Inventory.ReadInventory("PlayerInventory.json");
        }
        
        playerInventory.SetManager(this);

        currentInventoryPanel = UIElements[0];

        foreach (GameObject UIElement in UIElements) {
            UIElement.SetActive(false);
        }
        UpdateAll();
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

    public void DepositInventory(string itemType = "") {
        if (itemType == "") {
            playerInventory.DepositInHub(hubInventoryManager);
            playerInventory.WriteInventory();
        }
        else {
            playerInventory.DepositOneType(itemType, hubInventoryManager);
        }
        UpdateAll();
    }

    public void Consume(string itemName, int amount) {
        playerInventory.ConsumeItem(gameObject.GetComponent<InteractWithItems>().GetItem(itemName), amount, hubInventoryManager);
        UpdateAll();
    }


    public void ChangeInventory(int _newInventorySpace) {
        playerInventory.maxWeight = _newInventorySpace;
        gameObject.GetComponent<PlayerInventoryManager>().ChangeInventoryUI(playerInventory);
        UpdateAll();
    }

    public bool AddItem(Item _item) {
        if (playerInventory.Add(_item))
        {  
            UpdateAll();
            return true;
        }
        return false; 
    }

    public void RemoveItemAt(int index) {
        playerInventory.Remove(index);
        UpdateAll();
    }

    public void UpdateAll() {
        UpdateInventoryUI(playerInventory);
        hubInventoryManager.UpdateAll();
    }

   

    public Inventory GetInventory() {
        return playerInventory;
    }

    public void AddItemInUI(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].GetComponent<Image>().sprite == null)
            {
                inventorySlots[i].GetComponent<PlayerButtonInfo>().SetItem(item, i);
                inventorySlots[i].GetComponent<Image>().sprite = item.sprite;
                inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 1f);
                return;
            }
        }
    }

    public void ClearItemInUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].GetComponent<PlayerButtonInfo>().ResetItem();
            inventorySlots[i].GetComponent<Image>().sprite = null;
            inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 0f);
        }
    }

    public void ChangeInventoryUI(Inventory _playerInventory)
    {
        GameObject temp = UIElements[0];
        UIElements[0] = UIElements[1];
        UIElements[1] = temp;
        ShowUI();
        currentInventoryPanel.SetActive(false); //On éteind l'ancien panel
        currentInventoryPanel = UIElements[0];  //On change le current panel
        currentInventoryPanel.SetActive(true);  // Et on le rallume

        inventorySlots = new GameObject[_playerInventory.maxWeight];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = GameObject.Find("bagInventorySlots").transform.GetChild(i).gameObject;
        }

        UpdateInventoryUI(_playerInventory);

    }

    public void UpdateInventoryUI(Inventory playerInventory)
    {
        ClearItemInUI();
        Item[] inventoryTemp = playerInventory.listContent();
        foreach (Item item in inventoryTemp) {
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
            pointerUI.SetActive(false);
            currentInventoryPanel.SetActive(true);
        }
    }

    public bool IsUIOpen() {
        return currentInventoryPanel.activeSelf;
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

    
    public class Inventory {

        public List<Item> content;
        public int maxWeight;
        public int currentWeight;
        public string filename;

        public PlayerInventoryManager playerInventoryManager;
        
        public Inventory(int newInventorySpace, string _filename) {
            content = new List<Item>();
            currentWeight = 0;
            maxWeight = newInventorySpace;
            filename = _filename;
        }

        public void SetManager(PlayerInventoryManager _playerInventoryManager) {
            playerInventoryManager = _playerInventoryManager;
            WriteInventory();
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

        public void ClearInventory() {
            content = new List<Item>();
            currentWeight = 0;
            WriteInventory();
        }

        public bool Add(Item item) {
            if (currentWeight + item.weight <= maxWeight)
            {
                content.Add(item);
                currentWeight += item.weight;
                WriteInventory();
                return true;
            }
            return false;
        }

        public void Remove(int index) {
            currentWeight -= content[index].weight;
            content.RemoveAt(index);
            WriteInventory();
            
        }

        public void DepositInHub(HubInventoryManager hubInventoryManager) {
            foreach(Item item in playerInventoryManager.GetInventory().content) {
                hubInventoryManager.ChangeValue(item, 1);
            }
            ClearInventory();
            WriteInventory();
        }

        public void DepositOneType(string itemType, HubInventoryManager hubInventoryManager) {
            int total = 0;
            List<int> indexes = new List<int>();
            Item currentItem = new Item();
            for(int i = 0; i < content.Count; i++) { 
                if (content[i]._name == itemType) {
                    currentItem = content[i];
                    indexes.Add(i - total);
                    total += 1;
                }
            }
            foreach (int index in indexes) {
                content.RemoveAt(index);
            }
            currentWeight -= total * currentItem.weight;
            hubInventoryManager.ChangeValue(currentItem, total);
            WriteInventory();
        }

        public void ConsumeItem(Item item, int amount, HubInventoryManager hubInventoryManager) {
            int total = 0;
            List<int> indexes = new List<int>();
            for (int i = 0; i < content.Count; i++) {
                if (amount > 0) {
                    if (content[i]._name == item._name) {
                        indexes.Add(i - total);
                        total += 1;
                        amount -= 1;
                    }
                }
            }
            foreach (int index in indexes) {
                content.RemoveAt(index);
            }
            currentWeight -= total * item.weight;
            if (amount > 0) {
                hubInventoryManager.ChangeValue(item, -amount);
            }
            WriteInventory();
        }

        public int CountItem(string _itemName) {
            int returnValue = 0;
            if (content.Count == 0) {
                return returnValue;
            }
            foreach (Item item in content) {
                if (item._name == _itemName) {
                    returnValue += 1;
                }
            }
            return returnValue;
        }
        

        public Item[] listContent() {
            return content.ToArray();
        }

    }


}











