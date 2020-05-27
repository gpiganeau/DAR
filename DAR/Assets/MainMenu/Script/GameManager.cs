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
    public KeyCode eating{get;set;}
    public KeyCode check{get;set;}
    public KeyCode put{get;set;}
    public KeyCode quitArea{get;set;}

    void Start()
    {
        if(GM == null)
        {
            DontDestroyOnLoad(transform.gameObject);
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
        eating = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("eatKey","Mouse1"));
        check = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("checkKey","A"));
        put = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("putKey","Mouse0"));
        quitArea = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("quitAreaKey","R"));
    }

}
