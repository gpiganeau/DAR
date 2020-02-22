using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maximum_fog : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    private float densityValue;
    //private float shadowsValue;
    // Start is called before the first frame update
    void Start()
    {
        densityValue = GetComponentInParent<Aura2API.AuraVolume>().densityInjection.strength;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        directionalLight.shadowStrength = 0.1f;
        directionalLight.intensity = 0.4f;
    }
}
