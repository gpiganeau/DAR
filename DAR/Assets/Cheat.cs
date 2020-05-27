using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    bool inUse;
    public Item[] radioPieces;
    Transform spawnTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.C)) && !inUse) {
            inUse = true;
            
            for (int i = 0; i<radioPieces.Length;i++) {
                Instantiate<GameObject>(radioPieces[i].prefab, new Vector3(814.1f + i, 102.0405f, 845.8f), new Quaternion());
            }
            Debug.Log("Cheater");
        }
        else if (!Input.anyKey) {
            inUse = false;
        }
    }
}
