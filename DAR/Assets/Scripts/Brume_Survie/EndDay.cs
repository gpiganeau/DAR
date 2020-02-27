using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndDay : MonoBehaviour
{
    InGameDay loadedDay;

    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        string jsonDay = File.ReadAllText(Application.dataPath + "/JSONFiles/CurrentDay.json");
        loadedDay = JsonUtility.FromJson<InGameDay>(jsonDay);
        StartDay(loadedDay.day);
    }

    public void StartDay(int dayNumber) {
        canvas.GetComponentInChildren<TextMeshProUGUI>().text = "Day " + loadedDay.day.ToString();
        return;
    }


    public void EndThisDay() {
        StartCoroutine(EndDayCoroutine());
    }

    IEnumerator EndDayCoroutine() {
        canvas.GetComponent<Animator>().SetBool("StartFadeToBlack", true);
        yield return new WaitForSeconds(3.5f);

        loadedDay.day += 1;
        string uploadDay = JsonUtility.ToJson(loadedDay);
        File.WriteAllText(Application.dataPath + "/JSONFiles/CurrentDay.json", uploadDay);

        StartDay(loadedDay.day);

    }

    private class InGameDay {
        public bool gameHasStarted;
        public int day;
    }
}
