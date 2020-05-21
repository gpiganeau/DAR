using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public GameObject infoPanel;
    public Text infoText;
    Coroutine showInfoCoroutineReference;
    public Color originalColor;

    void Start()
    {
        HideInfo();
    }

    IEnumerator ShowInfoCoroutine(string info, float timer){
        float infoTimer = timer;
        infoText.text = info;
        infoText.color = originalColor;
        while ( infoTimer > 2 )
        {
            infoTimer -= Time.deltaTime;
            yield return null;
        }
        while (infoTimer > 0)
        {
            //change color
            infoTimer -= Time.deltaTime;
            infoText.color = Color.Lerp(originalColor, Color.clear, 1 - infoTimer/2);
            yield return null;
        }
        HideInfo();
    }

    public void ShowInfo(string info)
    {
        HideInfo();
        showInfoCoroutineReference = StartCoroutine(ShowInfoCoroutine(info, 3f));
    }

    public void HideInfo()
    {
        if (showInfoCoroutineReference != null)
        {
            StopCoroutine(showInfoCoroutineReference);
        }
        infoText.text = "";
    }
}
