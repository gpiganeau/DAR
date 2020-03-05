using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit = 600.0f;
    private float currentTime;
    public Text timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        string minSec = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
        timerText.text = minSec;
        if (currentTime <= 0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        TreasureHunter actualGame = GetComponent<TreasureHunter>();
        if (actualGame)
        {
            actualGame.PlayerLoses();
        }
    }
}
