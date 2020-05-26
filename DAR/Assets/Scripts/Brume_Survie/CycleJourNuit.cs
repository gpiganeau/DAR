using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CycleJourNuit : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private float dayLength;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject timerText;
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    bool playerIsInside;
    private Coroutine coroutineReference;
    public float timer;
    private float eveningLength;

    Quaternion originalRotation;

    //Partie son
    [FMODUnity.EventRef]
    public string dayMusicEvent = "";
    public FMOD.Studio.EventInstance dayMusic;

    public float StartingMusic = 3;
    float musicState;

    void Start() {
        eveningLength = dayLength / 5;
        originalRotation = transform.rotation;
        playerIsInside = player.GetComponent<PlayerStatus>().GetShelteredStatus();

        musicState = StartingMusic;

        dayMusic = FMODUnity.RuntimeManager.CreateInstance(dayMusicEvent);
        dayMusic.setParameterByName("musique", musicState);
        dayMusic.start();
    }

    public void PlayOneDay() {
        coroutineReference = StartCoroutine(OneDayCoroutine());
        dayMusic.setParameterByName("musique", StartingMusic);
    }

    public void StopDayCoroutine() {
        StopCoroutine(coroutineReference);
    }

    public void NightScene() {
        musicState = 2f;
        dayMusic.setParameterByName("musique", musicState);
        StartCoroutine(NightSceneCoroutine());
    }

    private void GameOver(bool worse) {
        int freezingLevel = player.GetComponent<PlayerStatus>().GetFreezingLevel();
        if (worse) {
            freezingLevel = player.GetComponent<PlayerStatus>().IncrementFreezingLevel();
        }
        else {
            freezingLevel = player.GetComponent<PlayerStatus>().ResetFreezingLevel();
        }


        if ((freezingLevel > 1) || !player.GetComponent<PlayerStatus>().GetShelteredStatus()) {
            //SceneManager.LoadScene(3);
            StopAllPlayerEvents();
            player.GetComponent<template>().Hypothermie();
        }
        else {
            musicState = 3f;
            dayMusic.setParameterByName("musique", musicState);
            timerText.SetActive(false);
            player.GetComponent<PlayerStatus>().SetIsRestingStatus(true);
            //player.GetComponent<EndDay>().EndThisDayInside(); 
        }
    }

    // Update is called once per frame
    void Update() {

    }
                

    IEnumerator OneDayCoroutine() {
        bool part2 = false;
        float eveningLerpValue;
        RenderSettings.ambientIntensity = 0.7f;
        RenderSettings.reflectionIntensity = 1f;
        RenderSettings.skybox = daySkybox;
        player.transform.Find("Camera_Despo").GetComponent<Aura2API.AuraCamera>().frustumSettings.baseSettings.density = 0f;
        player.GetComponent<PlayerStatus>().SetIsRestingStatus(false);
        timerText.SetActive(false);
        GetComponent<Light>().intensity = 1;
        transform.rotation = originalRotation;
        float rotation = 0f;
        while(rotation < 180f) {
            if (player.GetComponent<PlayerStatus>().GetDeadByHunger()) {
                //GameOver(true);
                player.GetComponent<template>().Famine();
                break;
            }
            transform.RotateAround(transform.position, new Vector3(1,0,0), Time.deltaTime * 180 / (dayLength * 60));
            rotation += (Time.deltaTime * 180) / (dayLength * 60);
            
            if (rotation > 90f && !part2)
            {
                part2 = true;
                musicState = 1f;
                dayMusic.setParameterByName("musique", musicState);
            }
            if (rotation > 144f) {
                eveningLerpValue = (rotation - 144f) / 36f;
                RenderSettings.ambientIntensity = Mathf.Lerp(0.7f, 0.1f, eveningLerpValue);
                RenderSettings.reflectionIntensity = Mathf.Lerp(1f, 0.1f, eveningLerpValue);
                player.transform.Find("Camera_Despo").GetComponent<Aura2API.AuraCamera>().frustumSettings.baseSettings.density = Mathf.Lerp(0f, .4f, eveningLerpValue);

            }
            yield return null;
        }
        RenderSettings.skybox = nightSkybox;
        player.GetComponent<EndDay>().EndThisDayOutside();

    }

    IEnumerator NightSceneCoroutine() {
        
        timerText.SetActive(true);
        bool worseConditions = true;
        timer = 30f;
        GetComponent<Light>().intensity = 0;

        FMOD.Studio.EventInstance heartbeatSound = FMODUnity.RuntimeManager.CreateInstance("event:/Character/heartbeat");
        heartbeatSound.start();
        heartbeatSound.release();
        while (timer > 0) {
            timerText.GetComponent<TextMeshProUGUI>().text = timer.ToString("#.0");
            if (!(player.GetComponent<PlayerStatus>().GetShelteredStatus() && player.GetComponent<PlayerStatus>().GetWarmStatus())) {
                timer -= Time.deltaTime;
                yield return null;
            }
            else {
                worseConditions = false;
                heartbeatSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                break;
            }
        }
        GameOver(worseConditions);
    }
    void OnDestroy()
    {
        StopAllPlayerEvents();
    }

    void StopAllPlayerEvents()
    {
        dayMusic.release();
        FMOD.Studio.Bus playerBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

 
