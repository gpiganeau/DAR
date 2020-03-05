using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bedPosition;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                {
                    player.transform.position = bedPosition.transform.position;
                    player.transform.rotation = bedPosition.transform.rotation;
                }
            }
        }
    }
}