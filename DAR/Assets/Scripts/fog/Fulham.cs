using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fulham : MonoBehaviour
{
    [SerializeField] private GameObject foret;
    [SerializeField] private GameObject auraVolumeOfIllusion; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        foret.SetActive(true);
        auraVolumeOfIllusion.SetActive(false);
    }
}
