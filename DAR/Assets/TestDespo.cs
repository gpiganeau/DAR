using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDespo : MonoBehaviour
{
    public GameObject Arm_List;
    public GameObject TaskList;
    public GameObject NoteList;

    public GameObject done_Wood;
    public GameObject done_AroundHouse;
    public GameObject newNote;
    public GameObject newNoteUI;
    public GameObject newTask;
    public GameObject Wood;
    public GameObject lightDay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Active la main et la TaskList et désactive la NoteList au cas où elle aurait été activé précédemment
        if (Input.GetKeyDown(KeyCode.Tab) && Arm_List.activeSelf == false)
        {
            Arm_List.SetActive(true);
            TaskList.SetActive(true);
            NoteList.SetActive(false);

        }
        //Désactive la main
        else if (Input.GetKeyDown(KeyCode.Tab) && Arm_List.activeSelf == true)
        {
            Arm_List.SetActive(false);
            newNoteUI.SetActive(false);
        }

        //Affiche la TaskList
        if (Input.GetKeyDown(KeyCode.Alpha1) && Arm_List.activeSelf == true)
        {
            TaskList.SetActive(true);
            NoteList.SetActive(false);
        }

        //Affiche la NoteList
        if (Input.GetKeyDown(KeyCode.Alpha2) && Arm_List.activeSelf == true)
        {
            TaskList.SetActive(false);
            NoteList.SetActive(true);
        }

        //Active la validation de la tâche get some wood
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            done_Wood.SetActive(true);
            Wood.SetActive(false);
        }

        //Active la validation de la tâche explore around house
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            done_AroundHouse.SetActive(true);
        }

        //Fait apparaître une nouvelle note
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            newNote.SetActive(true);
            newNoteUI.SetActive(true);
            newTask.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Arm_List.SetActive(false);
            lightDay.SetActive(false);
        }

    }
}
