using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jitter_lights : MonoBehaviour {
    Color hue;
    Light myLight;
    float valueValue;
    bool inCoroutine;
    [SerializeField] float darkTime;
    [SerializeField] float brightTime;
    // Start is called before the first frame update
    void Start() {
        myLight = GetComponent<Light>();

        hue = Random.ColorHSV(23f / 360f, 33f / 360f, 0.5f, 0.58f, 0.99f, 1f);
        myLight.color = hue;
        

    }

    // Update is called once per frame
    void Update() {
        if (!inCoroutine) {
            inCoroutine = true;
            darkTime = Random.Range(0, 0.5f);
            brightTime = Random.Range(0.3f, 1.5f);
            myLight.gameObject.SetActive(!myLight.gameObject.activeSelf);
            StartCoroutine(Blink(darkTime));
            StartCoroutine(Blink(brightTime));
            inCoroutine = false;
        }

    }

    IEnumerator Blink(float Time) {

        myLight.gameObject.SetActive(!myLight.gameObject.activeSelf);
        yield return new WaitForSeconds(Time);

    }
}
