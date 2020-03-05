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
    bool playerIsInside;
    private Coroutine coroutineReference;
    public float timer;


    Quaternion originalRotation;

    void Start() {
        originalRotation = transform.rotation;
        playerIsInside = player.GetComponent<PlayerStatus>().GetShelteredStatus();
    }

    public void PlayOneDay() {
        Debug.Log("DayStarting");
        coroutineReference = StartCoroutine(OneDayCoroutine());
    }

    public void StopDayCoroutine() {
        StopCoroutine(coroutineReference);
    }

    public void NightScene() {
        StartCoroutine(NightSceneCoroutine());
    }

    private void GameOver(bool gameOver) {
        if (gameOver) {
            SceneManager.LoadScene(2);
        }
        else {
            player.GetComponent<EndDay>().EndThisDayInside(); //À finir
        }
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator OneDayCoroutine() {
        timerText.SetActive(false);
        GetComponent<Light>().intensity = 1;
        transform.rotation = originalRotation;
        float rotation = 0f;
        while(rotation < 180f) {
            transform.RotateAround(transform.position, new Vector3(1,0,0), Time.deltaTime * 180 / (dayLength * 60));
            rotation += (Time.deltaTime * 180) / (dayLength * 60);
            yield return null;
        }
        if (!player.GetComponent<PlayerStatus>().GetShelteredStatus()) {
            player.GetComponent<EndDay>().EndThisDayOutside();
        }
        else {
            player.GetComponent<EndDay>().EndThisDayInside();
        }
        
    }

    IEnumerator NightSceneCoroutine() {
        timerText.SetActive(true);
        bool gameOver = true;
        timer = 30f;
        GetComponent<Light>().intensity = 0;
        while (timer > 0) {
            timerText.GetComponent<TextMeshProUGUI>().text = timer.ToString("#.0");
            if (!(player.GetComponent<PlayerStatus>().GetShelteredStatus() && player.GetComponent<PlayerStatus>().isWarm)) {
                timer -= Time.deltaTime;

                yield return null;
            }
            else {
                gameOver = false;
                break;
            }
        }
        GameOver(gameOver);
    }

}
