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


    // Start is called before the first frame update
    void Start()
    {
        string jsonDay = File.ReadAllText(Application.dataPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        StartDay(loadedDay.day);
        playerInventory = gameObject.GetComponent<InteractWithItems>();
        playerStatus = gameObject.GetComponent<PlayerStatus>();
    }

    public void StartDay(int dayNumber) {
        canvas.GetComponentInChildren<TextMeshProUGUI>().text = "Day " + loadedDay.day.ToString();
        canvas.GetComponent<Animator>().SetBool("StartFadeToBlack", false);
        sun.GetComponent<CycleJourNuit>().PlayOneDay();
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
        canvas.GetComponent<Animator>().SetBool("StartFadeToBlack", true);
        yield return new WaitForSeconds(3.5f);

        snowTracksManager.ResetTracks();
        playerInventory.ConsumeRessources();
        playerStatus.GoHome();

        loadedDay.day += 1;
        string uploadDay = JsonUtility.ToJson(loadedDay);
        File.WriteAllText(Application.dataPath + "/JSONFiles/CurrentDay.json", uploadDay);

        StartDay(loadedDay.day);
        if (!isInside) {
            playerInventory.LoseInventory();
        }
        
    }

    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }


}
