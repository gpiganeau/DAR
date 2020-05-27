using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    bool inUse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.C)) && !inUse) {
            inUse = true;
            Debug.Log("Cheater");
        }
        else if (!Input.anyKey) {
            inUse = false;
        }
    }
}
