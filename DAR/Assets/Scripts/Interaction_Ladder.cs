using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Ladder : MonoBehaviour
{

    float colorValue;
    bool onTop;
    GameObject[] lights;
    Animator[] children;

    GameObject[] small_lights;


    [SerializeField] private Transform top_position;
    [SerializeField] private Transform bottom_position;

    [SerializeField] private GameObject Lumiere1;
    [SerializeField] private GameObject Lumiere2;

    // Start is called before the first frame update
    void Start()
    {
        
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
            GameObject.Find("Camera_Despo").GetComponent<CameraController>().StartCinematic();
            StartCoroutine(Cinematic());

            small_lights = GameObject.FindGameObjectsWithTag("small_lights");
            foreach (GameObject small_light in small_lights) {
                small_light.GetComponent<twinkleTwinkleLittleStar>().Starting();
            }
            
        }
        
        
    }
    private IEnumerator Cinematic() {
        //Waiting 2 seconds for the cinematic to start
        yield return new WaitForSeconds(2);
        //Changing the light of the first Tower
        children = GetComponentsInChildren<Animator>();
        //getting all the components that need to be changed in the first tower
        foreach (Animator child in children) {
            child.SetBool("isFinale", true);
        }

        //Waiting for the cinematic to continue a bit and the other towers to show
        yield return new WaitForSeconds(2);
        //working on second tower
        children = Lumiere1.GetComponentsInChildren<Animator>();
        foreach (Animator child in children) {
            child.SetBool("isFinale", true);
        }
        //Same thing for the third tower
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        children = Lumiere2.GetComponentsInChildren<Animator>();
        foreach (Animator child in children) {
            child.SetBool("isFinale", true);
        }

        //Wait for first lighthouse to be out of sight to remove its light
        yield return new WaitForSeconds(4);
        GameObject[] cones = GameObject.FindGameObjectsWithTag("cone_to_remove");
        foreach (GameObject cone in cones) {
            cone.SetActive(false);
        }

        //Wait for empty space to be reached
        yield return new WaitForSeconds(5);
        GameObject.Find("Canvas").GetComponent<ShowTitleCard>().Show();
    }

}
