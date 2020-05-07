﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskState : MonoBehaviour
{
    public Tasks currentTask;
    public taskManager taskManagerObject;
    private GameObject title;
    private GameObject checkmark;
    public GameObject detail;
    private bool isCompleted;

    

    public void SelectTask()
    {
        taskManagerObject.SelectTask(currentTask);
    }

    // Start is called before the first frame update
    public void Initiate(Tasks task1,taskManager _taskManagerObject)
    {
        currentTask = task1;
        title = transform.GetChild(0).gameObject;
        checkmark = transform.GetChild(2).gameObject;
        taskManagerObject = _taskManagerObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        title.GetComponent<TMPro.TextMeshProUGUI>().text = currentTask._name;

        
        isCompleted = currentTask._isCompleted;
        if (isCompleted)
        {
            checkmark.SetActive(true);
        }
        else
        {
            checkmark.SetActive(false);
        }
    }


}
