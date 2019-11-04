using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log ("Scene 1");
			SceneManager.LoadScene(1);
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			Debug.Log ("Scene 2");
			SceneManager.LoadScene(2);
		}
    }
}
