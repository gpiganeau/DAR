using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCraftedItem : MonoBehaviour
{
    private int nbOfSpawn;
    //private int cpt = 0;
    [SerializeField] private GameObject planche;
    [SerializeField] private GameObject[] spawnList;

    // Start is called before the first frame update
    void Start()
    {
        nbOfSpawn = 3;
        spawnList = new GameObject[nbOfSpawn];
        for (int i = 0; i < spawnList.Length; i++)
        {
            spawnList[i] = transform.GetChild(i).gameObject; 
        }
    }

    public void SpawnCraft(string itemName)
    {
        for (int cpt = 0; cpt< spawnList.Length ; cpt++)
        if (spawnList[cpt].transform.childCount == 0)
            {
            switch (itemName)
            {
                case "planche":
                    GameObject instantiated = GameObject.Instantiate(planche, spawnList[cpt].transform.position, planche.transform.rotation);
                    instantiated.transform.parent = spawnList[cpt].transform;
                    break;
            }
                break;

        }
    }

    private void Update()
    {
        
    }
}
