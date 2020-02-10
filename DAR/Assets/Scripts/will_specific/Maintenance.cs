using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintenance : MonoBehaviour
{
    [SerializeField] float ressource;
    [SerializeField] float maxRessource;
    float descentSpeed = 2;
    float riseSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        ressource = maxRessource;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
            if (Input.GetAxis("Recharge") > 0.5 || Input.GetButton("Recharge")) { //right mouse button
                ressource = Mathf.MoveTowards(ressource, maxRessource, riseSpeed * Time.deltaTime);
            }
            else {
                ressource -= descentSpeed * Time.deltaTime;
            }
        }
        else {
            ressource -= descentSpeed * Time.deltaTime;
        }

    }
}
