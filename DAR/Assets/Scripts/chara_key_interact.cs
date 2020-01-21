using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chara_key_interact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 x;
    public Camera player_camera;


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(new Vector2(0, 0));

            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                if (objectHit.tag == "interactible") {
                    objectHit.gameObject.GetComponent<Interaction>().Interact();
                }
            }
        }
    }
}
