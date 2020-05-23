using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour 
{
    public bool gamePaused = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    [SerializeField] private mouseLook despoMouseLook;
    public Text dayText;

    private CursorLockMode lockModeState;
    private bool mouseLookState;
    private bool m_isAxisInUse = false;
    public bool isDead = false;
    void Start()
    {
        despoMouseLook = GetComponentInChildren<mouseLook>();
        gamePaused = false;
        pausePanel.SetActive(false);
        pauseCanvasGroup.enabled = false;
        lockModeState = Cursor.lockState;
        mouseLookState = despoMouseLook.ignore;
    }
    void Update()
    {
        if(isDead == false)
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

        //Debug Save
            if (gamePaused && Input.GetKeyDown("s") == true)
            {
                SaveDay();
            }
        }
     }
    private void PauseGame()
    {
        lockModeState = Cursor.lockState;
        mouseLookState = despoMouseLook.ignore;
        
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        despoMouseLook.ignore = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseCanvasGroup.enabled = true;
        //Disable scripts that still work while timescale is set to 0
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CameraController>().enabled = false;
        GetComponentInChildren<Headbobber>().enabled = false;
        GetComponent<PlayerInventoryManager>().enabled = false;
    } 
    private void ContinueGame()
    {
        gamePaused = false;
        Cursor.lockState = lockModeState;
        despoMouseLook.ignore = mouseLookState;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseCanvasGroup.enabled = false;
        //enable the scripts again
        GetComponent<CharacterController>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<CameraController>().enabled = true;
        GetComponentInChildren<Headbobber>().enabled = true;
        GetComponent<PlayerInventoryManager>().enabled = true;
    }

    public void TurnBackTimeOnly()
    {
        Time.timeScale = 1;
    }

    public void SaveDay()
    {
        FindObjectOfType<EndDay>().SaveDayData();
    }
}
