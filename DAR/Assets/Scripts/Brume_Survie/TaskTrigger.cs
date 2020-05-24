using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField] private taskManager taskMng;
    public Tasks taskToAdd;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            taskMng.AddTask(taskToAdd.key);
            Destroy(this);
        }
    }
}
