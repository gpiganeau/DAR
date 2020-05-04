using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item", fileName = "NewItem")]
public class Item : ScriptableObject {

    public GameObject prefab;

    public string _name;
    public int weight;
    public Sprite sprite;

    public int eatInfo;
    public bool isEating;

}
