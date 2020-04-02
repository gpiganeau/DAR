using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Craft : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    [SerializeField]
    private GameObject child_nbInventory;
    [SerializeField]
    private GameObject child_nbToCraft;

    


    public int _nbToCraft;

    private int _nbInventory;
   

    private void Update()
    {
        _nbInventory = parent.GetComponent<UpdateCraftingInventoryUIData>().GetValues()[0];
        child_nbInventory.GetComponent<TMPro.TMP_Text>().text = _nbInventory.ToString();
        child_nbToCraft.GetComponent<TMPro.TMP_Text>().text = "/" + _nbToCraft.ToString();


        if (_nbInventory < _nbToCraft)
        {
            child_nbInventory.GetComponent<TMPro.TMP_Text>().color = Color.red;
            gameObject.GetComponent<Button>().interactable = false;
        }
        else if (_nbInventory >= _nbToCraft)
        {
            child_nbInventory.GetComponent<TMPro.TMP_Text>().color = Color.black;
            gameObject.GetComponent<Button>().interactable = true;
        }


    }
}
