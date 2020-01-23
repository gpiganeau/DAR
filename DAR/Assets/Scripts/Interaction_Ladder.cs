using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Ladder : MonoBehaviour
{
    float time;
    float colorValue;
    bool onTop;
    GameObject[] lights;
    Animator[] children;


    [SerializeField] private Transform top_position;
    [SerializeField] private Transform bottom_position;

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
        if (this.gameObject.name == "Ladder_Interaction") {
            if (!onTop) {
                onTop = true;
                GameObject.Find("unitychan").transform.position = top_position.position;
            }
            else {
                onTop = false;
                GameObject.Find("unitychan").transform.position = bottom_position.position;
            }
        }
        else if (this.gameObject.name == "Crystal") {
            lights = GameObject.FindGameObjectsWithTag("Light_to_change");
            this.GetComponent<Animator>().SetBool("isFinale", true);
            foreach (GameObject light in lights) {
                children = light.GetComponentsInChildren<Animator>();
                foreach (Animator child in children) {
                    child.SetBool("isFinale", true);
                }
            }
            
        }
        
        
    }

}
