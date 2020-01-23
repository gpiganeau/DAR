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
        while (true) {
            if (!inCoroutine) {
                StartCoroutine(Blink());
            }
        }
        


    }

    // Update is called once per frame
    void Update() {


    }

    IEnumerator Blink() {
        while (true) {
            darkTime = Random.Range(0, 0.5f);
            brightTime = Random.Range(0.3f, 1.5f);
            myLight.gameObject.SetActive(!myLight.gameObject.activeSelf);
            Debug.Log("mais celle ci");
            yield return new WaitForSeconds(darkTime);
            Debug.Log("Pas celle là");
            myLight.gameObject.SetActive(!myLight.gameObject.activeSelf);
            yield return new WaitForSeconds(brightTime);

        }
    }
}
