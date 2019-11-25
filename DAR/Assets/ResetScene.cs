using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CheatResetGame")) {
            name = SceneManager.GetActiveScene().name;
            Debug.Log("I'm in!");
            //SceneManager.UnloadSceneAsync(name);
            SceneManager.LoadScene(name);
        }
    }
}
