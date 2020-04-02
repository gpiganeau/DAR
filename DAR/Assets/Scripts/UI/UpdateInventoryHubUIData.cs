using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInventoryHubUIData : MonoBehaviour
{

    public Text _nb_Wood;
    public Text _nb_Mushroom;
    // Start is called before the first frame update

    void Start()
    {

    }

    private void OnEnable() {
        if(_nb_Wood == null || _nb_Mushroom == null) {
            _nb_Wood = transform.Find("nb_Wood").GetComponent<Text>();
            _nb_Mushroom = transform.Find("nb_Mushroom").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValues(HubInventoryManager.Inventory hubInventory) {
        _nb_Wood.text = "x " + hubInventory.Count("wood");
        _nb_Mushroom.text = "x " + hubInventory.Count("mushroom");
    }

}
