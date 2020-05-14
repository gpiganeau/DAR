using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingScript : MonoBehaviour
{
    public Item fish1;
    public Item fish2;
    public Item fish3;

    [SerializeField] PlayerInventoryManager PlayerInventoryManager;

    Coroutine fishingCoroutine;
    Coroutine catchingFishCoroutine;
    float timer;
    float randomOffsetTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFishing() {
        if (fishingCoroutine == null) {
            fishingCoroutine = StartCoroutine(FishingCoroutine());
        }
        
    }

    public void StopFishing(int success) {
        timer = 0f;
        randomOffsetTime = -1f;
        if (success == 1) {
            PlayerInventoryManager.AddItem(Instantiate(fish1));
        }
        else if (success == 2) {
            PlayerInventoryManager.AddItem(Instantiate(fish2));
        }
        else if (success == 3) {
            PlayerInventoryManager.AddItem(Instantiate(fish3));
        }
        StopCoroutine(fishingCoroutine);
        fishingCoroutine = null;
    }

    IEnumerator CatchFish() {
        int success = 0;
        int fish;
        float pickUpTimer = 0f;
        float timeLimit = -1f;
        float randomFish = Random.value;
        if (randomFish < 0.75) {
            timeLimit = 3f;
            fish = 1;
        }
        else if(randomFish < 0.97) {
            timeLimit = 1.5f;
            fish = 2;
        }
        else {
            timeLimit = 0.7f;
            fish = 3;
        }
        while (pickUpTimer < timeLimit) {
            pickUpTimer += Time.deltaTime;
            //afficher indicateur
            if (Input.GetButtonDown("Interact")) {
                success = fish;
                break; 
            }
            else {
                yield return null;
            }
            yield return null;
        }
        //hide indicateur
        StopFishing(success);
    }

    IEnumerator FishingCoroutine() {
        float timer = 0f;
        while (timer < 9f) {
            timer += Time.deltaTime;
            if (randomOffsetTime < 0) {
                randomOffsetTime = Random.value * 5f;
                yield return null;
            }
            else if (timer > (3f + randomOffsetTime)) {
                Debug.Log("Now");
                yield return CatchFish();
            }
            yield return null;
        }
    }
}
