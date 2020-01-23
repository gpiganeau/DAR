using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_snow_particle : MonoBehaviour
{
    GameObject Snowparticle;
    // Start is called before the first frame update
    void Start()
    {
        Snowparticle = GameObject.Find("Snow_Particles_New");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Snowparticle.SetActive(false);
    }

    private void OnTriggerExit(Collider other) {
        Snowparticle.SetActive(true);
    }
}
