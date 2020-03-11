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
            timerText.SetActive(false);
            player.GetComponent<PlayerStatus>().SetIsRestingStatus(true);
            //player.GetComponent<EndDay>().EndThisDayInside(); 
        }
    }

    // Update is called once per frame
    void Update() {

    }
                

    IEnumerator OneDayCoroutine() {
        RenderSettings.ambientIntensity = 0.7f;
        RenderSettings.reflectionIntensity = 1f;
        player.GetComponent<PlayerStatus>().SetIsRestingStatus(false);
        timerText.SetActive(false);
        GetComponent<Light>().intensity = 1;
        transform.rotation = originalRotation;
        float rotation = 0f;
        while(rotation < 180f) {
            transform.RotateAround(transform.position, new Vector3(1,0,0), Time.deltaTime * 180 / (dayLength * 60));
            rotation += (Time.deltaTime * 180) / (dayLength * 60);
            yield return null;
        }
        player.GetComponent<EndDay>().EndThisDayOutside();

    }

    IEnumerator NightSceneCoroutine() {
        timerText.SetActive(true);
        bool gameOver = true;
        timer = 30f;
        GetComponent<Light>().intensity = 0;
        while (timer > 0) {
            RenderSettings.ambientIntensity = Mathf.MoveTowards(RenderSettings.ambientIntensity, 0.1f, Time.deltaTime / (5 * dayLength) );
            RenderSettings.reflectionIntensity = Mathf.MoveTowards(RenderSettings.reflectionIntensity, 0.1f, Time.deltaTime);
            timerText.GetComponent<TextMeshProUGUI>().text = timer.ToString("#.0");
            if (!(player.GetComponent<PlayerStatus>().GetShelteredStatus() && player.GetComponent<PlayerStatus>().GetWarmStatus())) {
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
