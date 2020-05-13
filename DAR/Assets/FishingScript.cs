using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingScript : MonoBehaviour
{
    Coroutine fishingCoroutine;
    PlayerInventoryManager PlayerInventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFishing() {
        fishingCoroutine = StartCoroutine(FishingCoroutine());
    }

    public void StopFishing(bool success) {
        StopCoroutine(fishingCoroutine);
        if (success) {

        }
    }

    IEnumerator FishingCoroutine() {
        yield return null;
    }
}
