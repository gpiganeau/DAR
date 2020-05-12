using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskManager : MonoBehaviour
{
    [SerializeField] private PlayerInventoryManager playerInventoryManager;
    private Tasks currentTask;
    public GameObject detail;
    public GameObject taskPrefab;
    [SerializeField] private GameObject taskSlotInHierarchy;
    public List<GameObject> taskSlots;
    public List<Tasks> AllCurrentTasks;
    public List<Tasks> AllTasks;
    public Dictionary<string,Tasks> AllTasksDictionary;
   

    // Start is called before the first frame update
    void Start()
    {
        taskSlots = new List<GameObject>(transform.childCount);
        foreach (Transform child in taskSlotInHierarchy.transform)
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
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            playerInventoryManager.ShowAlternateUI(4);            
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            
            AddTask(AllTasksDictionary["GSW"]);   
        }

        if (Input.GetKeyDown(KeyCode.N))
        {

            CompleteTask(currentTask);
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            DeleteTask(currentTask);
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
        testTaskGO.GetComponent<taskState>().Initiate(testTask,this);
        currentTask = testTask;
        currentTask._isCompleted = false;
        UpdateDetail(currentTask);

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
        if (currentTask._isCompleted) {
            detail.GetComponent<TMPro.TextMeshProUGUI>().text = task._detailFinish;
        }
        else {
            detail.GetComponent<TMPro.TextMeshProUGUI>().text = task._detail;
        }
        
    }

    public void SelectTask(Tasks task)
    {
        currentTask = task;
        UpdateDetail(currentTask);
        currentTask._isSelected = true;

        if (currentTask != task)
        {
            currentTask._isSelected = false;
            currentTask = task;
            UpdateDetail(currentTask);
            currentTask._isSelected = true;
        }
    }


}
