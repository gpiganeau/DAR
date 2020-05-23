﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public InfoManager infoManager;
    public bool isCatch;
    public bool beingCatch;
    public GameObject mText;
    public GameObject mText2;
    public Text mTextCatch;
    public GameObject rode;
    public GameObject thread; 
    public Animator anim;
    public Camera player_camera;
    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        x = Screen.width / 2;
        y = Screen.height / 2;
        mText.SetActive(false);
        //rode.SetActive(false);
        //infoManager = gameObject.GetComponent<InfoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCatch == true) {
            infoManager.ShowInfo("Poisson ajouté");
            isCatch = false;
        }
        anim.SetBool("isWin", false);

        if(beingCatch == true)
        {
            mText2.SetActive(false);
        }
    }

    public void StartFishing()
    {
        if (fishingCoroutine == null)
        {
            anim.SetBool("isStart",true);
            thread.SetActive(true);
            beingCatch = true;
            fishingCoroutine = StartCoroutine(FishingCoroutine());
        }
    }

    public void StopFishing(int success) {
        bool iBool = false;
        timer = 0f;
        anim.SetBool("isWin", true);
        anim.SetBool("isStart",false);
        randomOffsetTime = -1f;
        if(success == 0) {
            infoManager.ShowInfo("Vous n'avez pas attrapé de poisson");
        }
        else {
            if (success == 1) {
                iBool = PlayerInventoryManager.AddItem(Instantiate(fish1));
            }
            else if (success == 2) {
                iBool = PlayerInventoryManager.AddItem(Instantiate(fish2));
            }
            else if (success == 3) {
                iBool = PlayerInventoryManager.AddItem(Instantiate(fish3));
            }
            if (iBool) {
                infoManager.ShowInfo("Poisson ajouté");
            }
            else {
                infoManager.ShowInfo("Inventaire plein");
            }
        }
        
        StopCoroutine(fishingCoroutine);
        beingCatch = false;
        fishingCoroutine = null;
    }

    IEnumerator CatchFish(bool fail = false) {
        int success = 0;
        int fish;
        float pickUpTimer = 0f;
        float timeLimit = -1f;
        float randomFish = Random.value;
        if (fail) {
            isCatch = false;
            StopFishing(success);
        }
        else {
            if (randomFish < 0.75) {
                timeLimit = 3f;
                fish = 1;
            }
            else if (randomFish < 0.97) {
                timeLimit = 1.5f;
                fish = 2;
            }
            else {
                timeLimit = 0.8f;
                fish = 3;
            }
            while (pickUpTimer < timeLimit) {
                pickUpTimer += Time.deltaTime;
                anim.SetBool("isBite", true);
                mText.SetActive(true);
                //changer le clic gauche pour qu'il s'adapte à ce que le joueur a choisi
                mTextCatch.text = "Clic Gauche \n Pour attraper";
                if (Input.GetKeyDown(GameManager.GM.put) || (Input.GetAxis("Put") > 0.1)) {
                    success = fish;
                    isCatch = true;
                    break;
                }
                else {
                    isCatch = false;
                    yield return null;
                }
                yield return null;
            }
            anim.SetBool("isBite", false);
            mText.SetActive(false);
            yield return null;
            StopFishing(success);
            //hide indicateur
        }
        

    }

    IEnumerator FishingCoroutine() {
        bool missed = false;
        float timer = 0f;
        while (timer < 9f) {
            timer += Time.deltaTime;
            
            if (randomOffsetTime < 0) {
                randomOffsetTime = Random.value * 5f;
                yield return null;
            }
            else if((timer < (3f + randomOffsetTime)) && (Input.GetKeyDown(GameManager.GM.put) || (Input.GetAxisRaw("Put") > 0.1))){
                missed = true;
            }
            else if (timer > (3f + randomOffsetTime)) {
                yield return CatchFish();
            }
            if (missed) {
                yield return CatchFish(true);
            }
            else {
                yield return null;
            }
        }
    }
}
