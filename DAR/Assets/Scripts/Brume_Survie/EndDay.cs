﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndDay : MonoBehaviour
{
    InGameDay loadedDay;
    [SerializeField] Light sun;

    InteractWithItems playerInventory;
    [SerializeField] ResetTerrainOnDestroy resetTerrainOnDestroyObject;
    [SerializeField] GameObject blackFadeMachine;
    [SerializeField] TextMeshProUGUI currentDayText;
    private PlayerStatus playerStatus;
    [SerializeField] private GameObject firePlace;
    private Text dayTextPause;
    [SerializeField] private HungerSystem hungerSystem;
    [SerializeField] private taskManager taskMng;



    // Start is called before the first frame update
    
    void Awake()
    {
        taskMng = GameObject.Find("TaskManagerObject").GetComponent<taskManager>();
    }

    void Start()
    {
        string jsonDay = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        if (!dayTextPause) { dayTextPause = GetComponent<Pause>().dayText; }
        StartDay();
        playerInventory = gameObject.GetComponent<InteractWithItems>();
        playerStatus = gameObject.GetComponent<PlayerStatus>();
    }

    public void StartDay() {
        hungerSystem.StartInvoking();
        firePlace.GetComponent<FireplaceScript>().fireParticles.SetActive(false);
        firePlace.GetComponent<FireplaceScript>().fireplaceOn.SetActive(false);
        string currentDay = "Jour " + loadedDay.day.ToString();
        currentDayText.text = currentDay;
        blackFadeMachine.GetComponent<Animator>().SetBool("Fade", false);
        currentDayText.gameObject.GetComponent<Animator>().SetBool("StartFadeToBlack", false);
        sun.GetComponent<CycleJourNuit>().PlayOneDay();
        if (dayTextPause) { dayTextPause.text = currentDay; }
        if (Cursor.visible) { Cursor.visible = false; }
        AddDayTasks();
    }

    void AddDayTasks()
    {
        if (taskMng && loadedDay.day == 1)
        {
            taskMng.AddTask("GSW");
            taskMng.AddTask("GSF");
            taskMng.AddTask("GSH");
            taskMng.AddTask("FTB");
        }
    }

    public void EndOfDayLastMoments() {
        sun.GetComponent<CycleJourNuit>().NightScene();
    }


    public void EndThisDayOutside() {
        sun.GetComponent<CycleJourNuit>().StopDayCoroutine();
        StartCoroutine(EndDayCoroutine(false));
    }

    public void EndThisDayInside() {
        sun.GetComponent<CycleJourNuit>().StopDayCoroutine();
        StartCoroutine(EndDayCoroutine(true));
    }

    public void SaveDayData()
    {
        SaveSystem.SaveDay(loadedDay);
        Debug.Log("Day saved");
    }

    public void LoadDayData()
    {
        SaveData dayData = SaveSystem.LoadDay();

        loadedDay.day = dayData.day;
    }

    IEnumerator EndDayCoroutine(bool isInside) {

        if (isInside) {
            blackFadeMachine.GetComponent<Animator>().SetBool("Fade", true);
            currentDayText.gameObject.GetComponent<Animator>().SetBool("StartFadeToBlack", true);
            yield return new WaitForSeconds(3.5f);

            resetTerrainOnDestroyObject.ResetTracks();
            //playerInventory.ConsumeRessources(); No need to eat for the moment
            playerStatus.GoHome();

            loadedDay.day += 1;
            string uploadDay = JsonUtility.ToJson(loadedDay);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json", uploadDay);
            StartDay();
        }
        else {
            sun.GetComponent<CycleJourNuit>().NightScene();
        }
        
    }

    public class InGameDay {
        public bool gameHasStarted;
        public int day;
    }


}
