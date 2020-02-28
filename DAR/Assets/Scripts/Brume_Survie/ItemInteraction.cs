using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{

    

    [SerializeField] private string itemName;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Reset()
    {
        itemName = gameObject.GetComponent<Tool_Transform_To_Interractible>().GetName();        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public string GetName() {
        return itemName;
    }


    public void SetName(string name)
    {
        itemName = name;
    }
}
