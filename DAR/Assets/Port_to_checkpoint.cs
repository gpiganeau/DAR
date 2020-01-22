using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port_to_checkpoint : MonoBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) {
            GameObject.Find("unitychan").transform.position = checkpoints[0].position;
        }
        else if (Input.GetKeyDown(KeyCode.F2)) {
            GameObject.Find("unitychan").transform.position = checkpoints[1].position;
        }
        else if (Input.GetKeyDown(KeyCode.F3)) {
            GameObject.Find("unitychan").transform.position = checkpoints[2].position;
        }
        else if (Input.GetKeyDown(KeyCode.F4)) {
            GameObject.Find("unitychan").transform.position = checkpoints[3].position;
        }
        else if (Input.GetKeyDown(KeyCode.F5)) {
            GameObject.Find("unitychan").transform.position = checkpoints[4].position;
        }
    }
}
