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

    public void Open() {
        if (doorClosed.activeSelf) {

            doorClosed.gameObject.SetActive(false);
            doorOpened.gameObject.SetActive(true);

        }

        else if (doorOpened.activeSelf) {

            doorClosed.gameObject.SetActive(true);
            doorOpened.gameObject.SetActive(false);

        }
    }



}
