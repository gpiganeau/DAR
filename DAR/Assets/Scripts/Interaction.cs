using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    float time;
    float colorValue;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        while (time < 1) {
            colorValue = Mathf.Lerp(5, 95, time);
        }
    }

}
