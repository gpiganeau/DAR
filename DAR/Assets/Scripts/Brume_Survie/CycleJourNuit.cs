using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleJourNuit : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private float dayLength;
    [SerializeField] private GameObject player;
    bool playerIsInside;

    Quaternion originalRotation;

    void Start() {
        originalRotation = transform.rotation;
        playerIsInside = player.GetComponent<PlayerStatus>().GetShelteredStatus();
    }

    public void PlayOneDay() {
        StartCoroutine(OneDayCoroutine());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator OneDayCoroutine() {
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
}
