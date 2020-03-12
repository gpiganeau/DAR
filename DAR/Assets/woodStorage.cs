using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodStorage : MonoBehaviour
{

    public GameObject[] woodInStorage;

    // Start is called before the first frame update
    void Awake()
    {

        woodInStorage = new GameObject[transform.childCount];
        for (int i = 0; i < woodInStorage.Length; i++)
        {
            woodInStorage[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Show(int nbOfLog)
    {
        for (int i = 0; i < woodInStorage.Length; i++)
        {
            woodInStorage[i].SetActive(false);
        }
        for (int i = 0; i < Mathf.Min(nbOfLog, woodInStorage.Length); i++)
        {
            woodInStorage[i].SetActive(true);
        }
    }
}
