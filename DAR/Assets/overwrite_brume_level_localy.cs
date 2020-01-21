using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overwrite_brume_level_localy : MonoBehaviour
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
        GameObject.Find("Camera_Despo").GetComponent<Adapt_Brume>().OverrideBrumeValue(0.8f);
    }

    private void OnTriggerExit(Collider other) {
        GameObject.Find("Camera_Despo").GetComponent<Adapt_Brume>().StopOverride();
    }
}
