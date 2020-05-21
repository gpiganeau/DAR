using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    Vector2 center;
    float x;
    float y;
    public Camera player_camera;
    public float collection =0;

    [FMODUnity.EventRef]
    public string selectsound;
    FMOD.Studio.EventInstance soundevent;    
    // Start is called before the first frame update
    void Start()
    {
        soundevent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
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
                if (objectHit.tag == "color") {
                    Destroy(objectHit);
                    collection++;
                    soundevent.start();
                }
            }
        }
        if (collection >= 5)
        {
            Debug.Log ("You win");
        }
    }

    /*void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "color")
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log ("Touché");
                Destroy(collider.gameObject);
            }
        }
    }*/
}
