using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    GameObject gameControl;
    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("Game_Control");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        gameControl.GetComponent<fog_intensifies>().Waypoint_Discovered();
        gameControl.GetComponent<random_waypoints>().Destroying(this.gameObject);    
    }


}
