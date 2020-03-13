using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour 
{
    [SerializeField] private GameObject pausePanel;
    public Text dayText;
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
                m_isAxisInUse = true;
                if (!pausePanel.activeInHierarchy) 
                {
                    PauseGame();
                }
                else
                {
                    ContinueGame();
                }
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
        //Disable scripts that still work while timescale is set to 0
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CameraController>().enabled = false;
        GetComponentInChildren<Headbobber>().enabled = false;
    } 
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
        GetComponent<CharacterController>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<CameraController>().enabled = true;
        GetComponentInChildren<Headbobber>().enabled = true;
    }
}
