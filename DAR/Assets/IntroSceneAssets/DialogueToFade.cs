using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueToFade : MonoBehaviour
{
    public int sceneToLoad;
    public Image black;
    public Animator animBlack;
    
    public void GoToLoad()
    {
        StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        animBlack.SetBool("Fade", true);
        yield return new WaitUntil(()=>black.color.a == 1);
        //Debug.Log("Wait over");
        SceneManager.LoadScene(sceneToLoad);
    }
}
