using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Ladder : MonoBehaviour
{
    float time;
    float colorValue;
    bool onTop;

    public Transform top_position;
    public Transform bottom_position;

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
        if (!onTop) {
            onTop = true;
            GameObject.Find("unitychan").transform.position = top_position.position;
        }
        else {
            onTop = false;
            GameObject.Find("unitychan").transform.position = bottom_position.position;
        }
        
    }

}
