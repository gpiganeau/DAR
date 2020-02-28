using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpened;

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
            if (doorClosed.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    doorClosed.gameObject.SetActive(false);
                    doorOpened.gameObject.SetActive(true);
                }
            }

            else if (doorOpened.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    doorClosed.gameObject.SetActive(true);
                    doorOpened.gameObject.SetActive(false);
                }
            }
        }
    }

}
