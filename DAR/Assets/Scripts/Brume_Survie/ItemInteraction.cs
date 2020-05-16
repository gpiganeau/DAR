using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] private Item collectible;
    [SerializeField] private int numberOfUses = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfUses < 1) {
            Destroy(this.gameObject);
        }
    }

    public string GetName() {
        return itemName;
    }
    public void RemoveOneUse() {
        numberOfUses -= 1;
    }

    public Item GetCollectible() {
        if (collectible != null) {
            return collectible;
        }
        else return null;
    }

}
