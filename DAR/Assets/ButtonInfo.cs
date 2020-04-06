using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    private Item item;
    public HubInventoryManager hubInventoryManager;
    
    public void Transfer() {
        hubInventoryManager.ItemTransferHubToPlayer(item);
    }

    public void SetItem(Item _item) {
        item = _item;
    }
}
