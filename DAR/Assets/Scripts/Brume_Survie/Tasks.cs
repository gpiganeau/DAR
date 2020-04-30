using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task", fileName = "newTask")]
public class Tasks : ScriptableObject
{

    public GameObject prefabTask;
    public string key;
    public string _name;
    public string _detail;
    public string _detailFinish;
    public Sprite _drawing;
    public bool _isCompleted;

}
