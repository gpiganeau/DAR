using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public GameObject chestClosed;
    public GameObject chestOpened;
    public GameObject handleClosed;
    public GameObject handleOpened;
    

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Open() {
        if (chestClosed.activeSelf) {
            
            chestClosed.gameObject.SetActive(false);
            chestOpened.gameObject.SetActive(true);
            handleClosed.gameObject.SetActive(false);
            handleOpened.gameObject.SetActive(true); 
        }

        else {
            
            chestClosed.gameObject.SetActive(true);
            chestOpened.gameObject.SetActive(false);
            handleClosed.gameObject.SetActive(true);
            handleOpened.gameObject.SetActive(false);
        }
        
    }



}
