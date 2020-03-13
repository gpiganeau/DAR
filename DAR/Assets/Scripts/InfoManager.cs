using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public GameObject infoPanel;
    public Text infoText;

    void Start()
    {
        HideInfo();
    }

    IEnumerator ShowInfoCoroutine(string info, float timer){
        float infoTimer = timer;
        infoText.text = info;
        while ( infoTimer > 0 )
        {
            infoTimer -= Time.deltaTime;
            yield return null;
        }
        HideInfo();
    }

    public void ShowInfo(string info)
    {
        StartCoroutine(ShowInfoCoroutine(info, 3f));
    }

    public void HideInfo()
    {
        infoText.text = "";
    }
}
