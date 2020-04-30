using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskManager : MonoBehaviour
{
    private Tasks currentTask;
    public GameObject detail;
    public GameObject taskPrefab;
    public List<GameObject> taskSlots;
    public List<Tasks> AllCurrentTasks;
    public List<Tasks> AllTasks;
    public Dictionary<string,Tasks> AllTasksDictionary;

    // Start is called before the first frame update
    void Start()
    {
        taskSlots = new List<GameObject>(transform.childCount);
        foreach (Transform child in transform)
        {
            taskSlots.Add(child.gameObject);
        }

        AllTasksDictionary = new Dictionary<string, Tasks>();

        foreach (Tasks task in AllTasks)
        {
            Tasks newTask = Instantiate(task);
            AllTasksDictionary.Add(newTask.key, newTask);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            AddTask(AllTasksDictionary["GSW"]);   
        }

        if (Input.GetKeyDown(KeyCode.N))
        {

            CompleteTask(currentTask);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AddTask(AllTasksDictionary["GSH"]);
        }



    }

    public Tasks AddTask(Tasks _task)
    {
        GameObject gO = taskSlots[AllCurrentTasks.Count];
        Tasks testTask = Instantiate(_task, gO.transform);
        AllCurrentTasks.Add(testTask);
        GameObject testTaskGO = Instantiate(testTask.prefabTask, gO.transform);
        testTaskGO.GetComponent<taskState>().Initiate(testTask);
        UpdateDetail(_task);
        currentTask = testTask;

        return currentTask;
        
    }

    public void CompleteTask(Tasks task)
    {
        detail.GetComponent<TMPro.TextMeshProUGUI>().text = task._detailFinish;
        currentTask._isCompleted = true;
    }

    public void DeleteTask(Tasks task)
    {

    }

    public void UpdateDetail (Tasks task)
    {
        detail.GetComponent<TMPro.TextMeshProUGUI>().text = task._detail;
    }


}
