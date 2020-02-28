using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject chestClosed;
    public GameObject chestOpened;
    public GameObject handleClosed;
    public GameObject handleOpened;

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
            if (chestClosed.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    chestClosed.gameObject.SetActive(false);
                    chestOpened.gameObject.SetActive(true);
                    handleClosed.gameObject.SetActive(false);
                    handleOpened.gameObject.SetActive(true);
                }
            }

            else if (chestOpened.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    chestClosed.gameObject.SetActive(true);
                    chestOpened.gameObject.SetActive(false);
                    handleClosed.gameObject.SetActive(true);
                    handleOpened.gameObject.SetActive(false);
                }
            }
        }
    }

}
