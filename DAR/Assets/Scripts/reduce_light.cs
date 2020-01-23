using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reduce_light : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        GameObject.Find("Lumiere_Phare").GetComponent<Animator>().SetBool("isInTower", true);
    }
    private void OnTriggerExit(Collider other) {
        GameObject.Find("Lumiere_Phare").GetComponent<Animator>().SetBool("isInTower", false);
    }
}

