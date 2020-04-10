using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuToLoadScene : MonoBehaviour
{
    public int sceneToLoad;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void GoToLoad()
    {
        //StartCoroutine(Timer());
        SceneManager.LoadScene(1);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("Wait over");
        SceneManager.LoadScene(1);
    }
}
