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
    private bool m_isAxisInUse = false;
   

    // Start is called before the first frame update
    void Awake()
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            CompleteTask("GSW");
        }

        /*if (Input.GetKeyDown(KeyCode.Comma))
        {
            DeleteTask(currentTask);
        }*/

        if(Input.GetAxisRaw("Checklist") != 0) 
        {
            if(m_isAxisInUse == false)
            {
                m_isAxisInUse = true;
                playerInventoryManager.ShowAlternateUI(4);
            }
        }

        if(Input.GetAxisRaw("Checklist") == 0)
        {
            m_isAxisInUse = false;
        }

    }

    public Tasks AddTask(string taskKeyCode)
    {
        GameObject gO = taskSlots[AllCurrentTasks.Count];
        Tasks testTask = Instantiate(AllTasksDictionary[taskKeyCode], gO.transform);
        AllCurrentTasks.Add(testTask);
        GameObject testTaskGO = Instantiate(testTask.prefabTask, gO.transform);
        testTaskGO.GetComponent<taskState>().Initiate(testTask,this);
        currentTask = testTask;
        currentTask._isCompleted = false;
        UpdateDetail();

        return currentTask;
        
    }

    public void CompleteTask(string taskKeyCode)
    {
        Tasks targetTask = AllCurrentTasks.Find( x => x.key == taskKeyCode);
        targetTask._isCompleted = true;
        UpdateDetail();
    }

    public void DeleteTask(string taskKeyCode)
    {
        
    }

    public void UpdateDetail()
    {
        if (currentTask._isCompleted) {
            detail.GetComponent<TMPro.TextMeshProUGUI>().text = currentTask._detailFinish;
        }
        else {
            detail.GetComponent<TMPro.TextMeshProUGUI>().text = currentTask._detail;
        }
        
    }

    public void SelectTask(Tasks task)
    {
        currentTask = task;
        UpdateDetail();
        currentTask._isSelected = true;

        if (currentTask != task)
        {
            currentTask._isSelected = false;
            currentTask = task;
            UpdateDetail();
            currentTask._isSelected = true;
        }
    }


}
