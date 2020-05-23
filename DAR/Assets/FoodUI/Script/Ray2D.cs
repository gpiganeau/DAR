using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ray2D : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    Item itemSelected;

    void Start ()
    {
        // Récupère le Raycaster depuis le GameObject (le Canvas )
        m_Raycaster = GetComponent <GraphicRaycaster> ();
        // Récupérer le système d' événements de la scène
        m_EventSystem = GetComponent < EventSystem > ();
    } 

    void Update ()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            var buttonInfo = result.gameObject.GetComponent<PlayerButtonInfo>();
            if (buttonInfo) {
                if (Input.GetKeyUp(GameManager.GM.put) || (Input.GetAxis("Put") > 0.1)) {
                    buttonInfo.Select();
                }

                else if (Input.GetKeyUp(GameManager.GM.eating) || Input.GetButtonDown("Eating")) {
                    buttonInfo.Eat();
                }
            }
        }
    }
}
