using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chara_key_interact : MonoBehaviour
{
    Vector2 center;
    float x;
    float y;
    public Camera player_camera;
    // Start is called before the first frame update
    void Start()
    {
        x = Screen.width / 2;
        y = Screen.height / 2;
    }



    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
            if (Physics.Raycast(ray, out hit, 10, ~(1 << 11))) {
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.tag == "interactible") {
                    objectHit.gameObject.GetComponent<Interaction_Ladder>().Interact();
                }
            }
        }
    }
}
