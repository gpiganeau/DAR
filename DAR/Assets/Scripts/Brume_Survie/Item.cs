using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item", fileName = "NewItem")]
public class Item : ScriptableObject {

    public string prefabName;

    public string _name;
    public int weight;
    public Sprite sprite;
    

}
