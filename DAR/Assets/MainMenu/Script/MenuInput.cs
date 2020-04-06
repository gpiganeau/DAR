using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour
{
    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    void Start()
    {
        menuPanel = transform.Find("ControlsMenu");
        waitingForKey = false;

        for (int i =0; i<6; i++)
        {
            if(menuPanel.GetChild(i).name == "InteractionKey")
                {menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.interaction.ToString();}
            else if(menuPanel.GetChild(i).name == "InventoryKey")
                {menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.inventory.ToString();}
            else if(menuPanel.GetChild(i).name == "ForwardKey")
                {menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.forward.ToString();}
            else if(menuPanel.GetChild(i).name == "BackwardKey")
                {menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.backward.ToString();}
            else if(menuPanel.GetChild(i).name == "LeftKey")
               {menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();}
            else if(menuPanel.GetChild(i).name == "RightKey")
                {menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();}
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
            case "inventory":
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
        }

        yield return null;
    }
}
