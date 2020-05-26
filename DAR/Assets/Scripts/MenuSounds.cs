using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string ClickEvent = "";
    public void PlayClickSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ClickEvent);
    }
}
