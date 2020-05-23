using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour
{
    public Transform menuPanel1;
    public Transform menuPanel2;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    void Start()
    {

        waitingForKey = false;

        for (int i =0; i<6; i++)
        {
            if(menuPanel1.GetChild(i).name == "InteractionKey")
                {menuPanel1.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.interaction.ToString();}
            else if(menuPanel1.GetChild(i).name == "ForwardKey")
                {menuPanel1.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.forward.ToString();}
            else if(menuPanel1.GetChild(i).name == "BackwardKey")
                {menuPanel1.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.backward.ToString();}
            else if(menuPanel1.GetChild(i).name == "LeftKey")
               {menuPanel1.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();}
            else if(menuPanel1.GetChild(i).name == "RightKey")
                {menuPanel1.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();}
            else if(menuPanel1.GetChild(i).name == "EatKey")
                {menuPanel1.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.eating.ToString();}
        }

        for(int j=0; j<3; j++)
        {
            if(menuPanel2.GetChild(j).name == "InventoryKey")
                {menuPanel2.GetChild(j).GetComponentInChildren<Text>().text = GameManager.GM.inventory.ToString();}
            else if(menuPanel2.GetChild(j).name == "CheckKey")
                {menuPanel2.GetChild(j).GetComponentInChildren<Text>().text = GameManager.GM.check.ToString();}
            else if(menuPanel2.GetChild(j).name == "PutKey")
                {menuPanel2.GetChild(j).GetComponentInChildren<Text>().text = GameManager.GM.put.ToString();}
            /*else if(menuPanel2.GetChild(j).name == "quitAreaKey")
                {menuPanel2.GetChild(j).GetComponentInChildren<Text>().text = GameManager.GM.quitArea.ToString();}*/
        }
    }

    void OnGUI()
    {
        keyEvent = Event.current;
        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if(!waitingForKey)
        {StartCoroutine(AssignKey(keyName));}
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitingForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitingForKey();
        switch(keyName)
        {
            case "interaction" :
                GameManager.GM.interaction = newKey;
                buttonText.text = GameManager.GM.interaction.ToString();
                PlayerPrefs.SetString("interactionKey", GameManager.GM.interaction.ToString());
                break;
            case "inventory" :
                GameManager.GM.inventory = newKey;
                buttonText.text = GameManager.GM.inventory.ToString();
                PlayerPrefs.SetString("inventoryKey", GameManager.GM.inventory.ToString());
                break;
            case "forward" :
                GameManager.GM.forward = newKey;
                buttonText.text = GameManager.GM.forward.ToString();
                PlayerPrefs.SetString("forwardKey", GameManager.GM.forward.ToString());
                break;
            case "backward" :
                GameManager.GM.backward = newKey;
                buttonText.text = GameManager.GM.backward.ToString();
                PlayerPrefs.SetString("backwardKey", GameManager.GM.backward.ToString());
                break;
            case "left" :
                GameManager.GM.left = newKey;
                buttonText.text = GameManager.GM.left.ToString();
                PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString());
                break;
            case "right" :
                GameManager.GM.right = newKey;
                buttonText.text = GameManager.GM.right.ToString();
                PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString());
                break;
            case "eat" :
                GameManager.GM.eating = newKey;
                buttonText.text = GameManager.GM.eating.ToString();
                PlayerPrefs.SetString("eatKey", GameManager.GM.eating.ToString());
                break;
            case "put" :
                GameManager.GM.put = newKey;
                buttonText.text = GameManager.GM.put.ToString();
                PlayerPrefs.SetString("putKey", GameManager.GM.put.ToString());
                break;
            case "check" :
                GameManager.GM.check = newKey;
                buttonText.text = GameManager.GM.check.ToString();
                PlayerPrefs.SetString("checkKey", GameManager.GM.check.ToString());
                break;
            /*case "quitArea" :
                GameManager.GM.quitArea = newKey;
                buttonText.text = GameManager.GM.quitArea.ToString();
                PlayerPrefs.SetString("quiAreaKey", GameManager.GM.quitArea.ToString());
                break;*/
        }
        yield return null;
    }
}
