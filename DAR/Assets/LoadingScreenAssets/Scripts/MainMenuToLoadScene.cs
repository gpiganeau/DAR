using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuToLoadScene : MonoBehaviour
{
    public int sceneToLoad;
    public Image black;
    public Animator animBlack;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void GoToIntro()
    {
        StartCoroutine(Fading(4));
    }
    
    public void GoToLoad()
    {
        StartCoroutine(Fading(1));
    }

    private IEnumerator Fading(int sceneNumber)
    {
        animBlack.SetBool("Fade", true);
        yield return new WaitUntil(()=>black.color.a == 1);
        //Debug.Log("Wait over");
        SceneManager.LoadScene(sceneNumber);
    }
}
