using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonInfo : MonoBehaviour
{
    private Item item;
    private int index = -1;
    public ItemSetInEnvironment itemSetInEnvironment;

    public void Select() {
        if (item) {
            itemSetInEnvironment.PutInHand(item, index);
        }
    }

    public void SetItem(Item _item, int _index) {
        item = _item;
        index = _index;
    }

    public void ResetItem() {
        item = null;
        index = -1;
    }
}
