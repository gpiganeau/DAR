using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public Item item;
    public HubInventoryManager hubInventoryManager;
    
    public void Transfer() {
        hubInventoryManager.ItemTransferHubToPlayer(item);
    }
}
