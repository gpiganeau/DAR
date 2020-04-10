using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField]
    private Image _progressBar;
    [SerializeField]
    private MainMenuToLoadScene loadSceneInfo;
    [SerializeField]
    private int sceneIndex;
    
    void Awake()
    {
        //get the scene to load from MainMenuToLoadScene
        loadSceneInfo = GameObject.FindObjectOfType<MainMenuToLoadScene>();
        if (loadSceneInfo != null)
        {
            sceneIndex = loadSceneInfo.sceneToLoad;
        }
        else sceneIndex = 0;
    }
    
    void Start()
    {
        //start async operation
        StartCoroutine(TimerBeforeLoad());
    }

    private IEnumerator TimerBeforeLoad()
    {
        yield return new WaitForSeconds(2.0f);
        //Debug.Log("Wait over");
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //create an async operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneIndex);

        while (gameLevel.progress < 1)
        {
            //take the progress bar fill = async operation progress
            _progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
        //when finished, load the game scene. It's automatic !
    }
}
