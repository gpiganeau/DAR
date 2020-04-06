using System.Collections;
using System.Collections.Generic;                                       
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    Slider sliderMusic;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float MasterVolume = 1f;

    void Awake()                                                    
    {
        Music = FMODUnity.RuntimeManager.GetBus ("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus ("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
    }

    void Start()
    {
        MusicVolume = PlayerPrefs.GetFloat("Music",0.5f);
        SFXVolume = PlayerPrefs.GetFloat("SFX",0.5f);
        MasterVolume = PlayerPrefs.GetFloat("Master",1f);
    }

    void Update()
    {
        Music.setVolume (MusicVolume);
        SFX.setVolume (SFXVolume);
        Master.setVolume (MasterVolume);
        PlayerPrefs.SetFloat("Music",MusicVolume);

    }

    public void MusicVolumeLevel (float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
        PlayerPrefs.SetFloat("Music", MusicVolume);
    }

    public void SFXVolumeLevel (float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
        PlayerPrefs.SetFloat("SFX", SFXVolume);
    }

    public void MasterVolumeLevel (float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
        PlayerPrefs.SetFloat("Master", MasterVolume);
    }
}
