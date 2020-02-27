using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleJourNuit : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private int dayLength;
    [SerializeField] private Transform worldLight;
    [SerializeField] private GameObject player;
    void Start() {
        StartCoroutine(OneDayCoroutine());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator OneDayCoroutine() {
        float rotation = 0f;
        while(rotation < 180f) {
            worldLight.RotateAround(worldLight.position, new Vector3(1,0,0), Time.deltaTime * 180 / (dayLength * 60));
            rotation += (Time.deltaTime * 180) / (dayLength * 60);
            yield return null;
        }
        player.GetComponent<EndDay>().EndThisDay();
    }
}
