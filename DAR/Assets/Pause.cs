using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool m_isAxisInUse = false;
    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetAxisRaw("Pause") != 0) 
        {
            if(m_isAxisInUse == false)
            {
                if (!pausePanel.activeInHierarchy) 
                {
                    PauseGame();
                    Debug.Log(pausePanel.activeInHierarchy);
                }
                else
                {
                    ContinueGame();
                    Debug.Log(pausePanel.activeInHierarchy);   
                }
                m_isAxisInUse = true;
            }
        }
        if( Input.GetAxisRaw("Pause") == 0)
        {
            m_isAxisInUse = false;
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        GetComponent<MapController>().enabled = false;
        //Disable scripts that still work while timescale is set to 0
    } 
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        GetComponent<MapController>().enabled = true;
        //enable the scripts again
    }
}
