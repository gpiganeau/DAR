using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    private string ClickEvent = "event:/Menu/Boutons menu";
    public void PlayClickSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ClickEvent);
    }
}
