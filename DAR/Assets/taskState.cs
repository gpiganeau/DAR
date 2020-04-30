using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskState : MonoBehaviour
{
    public Tasks currentTask;

    private GameObject title;
    private GameObject checkmark;
    public GameObject detail;
    private bool isCompleted;

    // Start is called before the first frame update
    public void Initiate(Tasks task1)
    {
        currentTask = task1;
        title = transform.GetChild(0).gameObject;
        checkmark = transform.GetChild(2).gameObject;
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
