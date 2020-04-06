using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInventoryHubUIData : MonoBehaviour
{

    public Text _nb_Wood;
    public Text _nb_Mushroom;
    private GameObject[] inventorySlots;
    // Start is called before the first frame update

    void Start()
    {
        inventorySlots = new GameObject[transform.childCount];
        for (int i = 0; i < inventorySlots.Length; i++) {
            inventorySlots[i] = transform.GetChild(i).gameObject;
        }
    }

    //private void OnEnable() {
    //    if(_nb_Wood == null || _nb_Mushroom == null) {
    //        _nb_Wood = transform.Find("nb_Wood").GetComponent<Text>();
    //        _nb_Mushroom = transform.Find("nb_Mushroom").GetComponent<Text>();
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValues(HubInventoryManager.Inventory hubInventory) {
        for (int i = 0; i < hubInventory.content.Count; i++) {
            inventorySlots[i].GetComponent<Image>().sprite = hubInventory.content[i].sprite;
            inventorySlots[i].GetComponent<Image>().color = new Color(inventorySlots[i].GetComponent<Image>().color.r, inventorySlots[i].GetComponent<Image>().color.g, inventorySlots[i].GetComponent<Image>().color.b, 1f);
            inventorySlots[i].GetComponentInChildren<Text>().text = "x " + hubInventory.amount[i];
            inventorySlots[i].GetComponent<ButtonInfo>().item = hubInventory.content[i];
        }
    }

}
