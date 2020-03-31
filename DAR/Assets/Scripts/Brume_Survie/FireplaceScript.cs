using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FireplaceScript : MonoBehaviour
{
    public GameObject fireplaceOn;
    public GameObject fireParticles;
    public GameObject player;
    HubInventoryManager hubInventoryManager;

    PlayerInventoryManager playerInventoryManager;

    void Start()
    {
 
    }

    void Update()
    {

    }

    public void Light() {

        if (fireplaceOn.activeSelf == false) {
            fireplaceOn.gameObject.SetActive(true);
            fireParticles.gameObject.SetActive(true);
            playerInventoryManager.Consume("wood", 3);
        }
    }
}
