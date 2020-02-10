using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelWind : MonoBehaviour
{
    GameObject GameController;
    DirectionalHazard hazardScript;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameController");
        hazardScript = GameController.GetComponent<DirectionalHazard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            hazardScript.windEnable = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "Player") {
            hazardScript.windEnable = true;
        }
    }
}
