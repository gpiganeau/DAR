using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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


    // Start is called before the first frame update
    void Start()
    {
        string jsonDay = File.ReadAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        StartDay();
        playerInventory = gameObject.GetComponent<InteractWithItems>();
        playerStatus = gameObject.GetComponent<PlayerStatus>();
    }

    public void StartDay() {
        firePlace.GetComponent<FireplaceScript>().fireParticles.SetActive(false);
        firePlace.GetComponent<FireplaceScript>().fireplaceOn.SetActive(false);
        canvas.GetComponentInChildren<TextMeshProUGUI>().text = "Day " + loadedDay.day.ToString();
        canvas.GetComponent<Animator>().SetBool("StartFadeToBlack", false);
        sun.GetComponent<CycleJourNuit>().PlayOneDay();
        
        
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

    IEnumerator EndDayCoroutine(bool isInside) {

        if (isInside) {
            canvas.GetComponent<Animator>().SetBool("StartFadeToBlack", true);
            yield return new WaitForSeconds(3.5f);

            snowTracksManager.ResetTracks();
            playerInventory.ConsumeRessources();
            playerStatus.GoHome();

            loadedDay.day += 1;
            string uploadDay = JsonUtility.ToJson(loadedDay);
            File.WriteAllText(Application.streamingAssetsPath + "/JSONFiles/CurrentDay.json", uploadDay);
            StartDay();
        }
        else {
            Debug.Log("NightScene should start");
            sun.GetComponent<CycleJourNuit>().NightScene();
        }
        
    }

    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }


}
