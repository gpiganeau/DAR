using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode interaction {get; set;}
    public KeyCode inventory {get; set;}
    public KeyCode forward {get; set;}
    public KeyCode backward {get; set;}
    public KeyCode left {get; set;}
    public KeyCode right {get; set;}

    void Awake()
    {
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }
        
        interaction = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("interactionKey","E"));
        inventory = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("inventoryKey","Tab"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("forwardKey","Z"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("backwardKey","S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("leftKey","Q"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("rightKey","D"));

       
    }

}
