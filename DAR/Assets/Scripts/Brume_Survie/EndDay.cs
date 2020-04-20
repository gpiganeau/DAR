using System.IO;
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
    [SerializeField] SnowOnTerrainManager snowTracksManager;
    [SerializeField] Canvas canvas;
    private PlayerStatus playerStatus;
    [SerializeField] private GameObject firePlace;
    private Text dayTextPause;
    


    // Start is called before the first frame update
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
        firePlace.GetComponent<FireplaceScript>().fireParticles.SetActive(false);
        firePlace.GetComponent<FireplaceScript>().fireplaceOn.SetActive(false);
        string currentDay = "Day " + loadedDay.day.ToString();
        canvas.GetComponentInChildren<TextMeshProUGUI>().text = currentDay;
        canvas.GetComponent<Animator>().SetBool("StartFadeToBlack", false);
        sun.GetComponent<CycleJourNuit>().PlayOneDay();
        if (dayTextPause) { dayTextPause.text = currentDay; }
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

    IEnumerator EndDayCoroutine(bool isInsideAndWarm) {

        if (isInsideAndWarm) {
            canvas.GetComponent<Animator>().SetBool("StartFadeToBlack", true);
            yield return new WaitForSeconds(3.5f);

            //snowTracksManager.ResetTracks();
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

    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }


}
