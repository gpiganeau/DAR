using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject choose;
    public GameObject parametreGraphism;
    public GameObject parametreControls;
    public GameObject parametreSound;    
    public GameObject parametreControlsPage1;
    public GameObject parametreControlsPage2;

//------------------------------[Partie Retour Menu Principal]-------------------------------------------------//

    public void BackMainMenu()
    {
        SceneManager.LoadScene("Scene0");
        /*choose.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);*/
    }

//------------------------------[Partie Sauvegarde]-----------------------------------------------------------//


    public void Save()
    {
        choose.SetActive(true);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
    }


//------------------------------[Partie Options]------------------------------------------------------------//

    public void Graphism()
    {
        choose.SetActive(false);
        parametreGraphism.SetActive(true);
        parametreSound.SetActive(false);
        parametreControls.SetActive(false);
    }

    public void Controls()
    {
        choose.SetActive(false);
        parametreControlsPage1.SetActive(true);
        parametreControlsPage2.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(true);
    }

    public void GoPage2()
    {
        choose.SetActive(false);
        parametreControlsPage1.SetActive(false);
        parametreControlsPage2.SetActive(true);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(false);
        parametreControls.SetActive(true);
    }

    public void Sound()
    {
        choose.SetActive(false);
        parametreGraphism.SetActive(false);
        parametreSound.SetActive(true);
        parametreControls.SetActive(false);
    }

//------------------------------[Partie Quitter]------------------------------------------------------------//

    public void QuitGame()
    {
        Application.Quit();
    }

//------------------------------[Partie Retour En Jeu ]----------------------------------------------------//

    public void BackGame()
    {
        //Script Muko
    }
}
