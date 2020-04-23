using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FireplaceScript : MonoBehaviour
{
    //public GameObject fireplaceOn;
    public GameObject fireParticles;
    public GameObject player;

    void Start()
    {
 
    }

    void Update()
    {

    }

    public void Light() {

        if (fireParticles.activeSelf == false) {
           // fireplaceOn.gameObject.SetActive(true);
            fireParticles.gameObject.SetActive(true);
            player.GetComponent<PlayerInventoryManager>().Consume("wood", 3);
        }
    }
}
