using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAppearScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update() {
        if (Input.GetKeyDown("m")) {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}
