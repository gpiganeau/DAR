using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxelScript : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image cross;
    public GameObject map;

    void Start()
    {
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
        image3.gameObject.SetActive(false);
        image4.gameObject.SetActive(false);
        cross.gameObject.SetActive(false);
        map.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            image1.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            image2.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            image3.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            image4.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cross.gameObject.SetActive(true);
        }
        if (map.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Quote))
            {
                map.gameObject.SetActive(false);
            }
        }
        if (!map.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Quote))
            {
                map.gameObject.SetActive(true);
            }
        }
    }
}
