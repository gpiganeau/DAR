using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task", fileName = "newTask")]
public class Tasks : ScriptableObject
{

    public GameObject prefabTask;
    public string key;
    public string _name;
    [TextArea(15, 20)]
    public string _detail;
    [TextArea(15, 20)]
    public string _detailFinish;
    public Sprite _drawing;
    public bool _isCompleted;
    public bool _isSelected;

}
