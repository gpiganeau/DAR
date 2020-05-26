using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HungerSystem : MonoBehaviour
{
    public Text mTextName;
    public Text mTextInteraction;
    public Text mTextName2;
    public Text mTextInteraction2;
    public Text mTextFood;
    public GameObject mText;
    public GameObject mText2;

    private Transform _selection;
    
    [SerializeField] private string foodTag = "Food";
    [SerializeField] private string interactiveTag = "interactible";
    [SerializeField] private PlayerStatus playerStatus;
    public Camera player_camera;


    public Hunger mFoodBar;
    private int startFood;

    public float rateHunger = 0.5f;
    public int food = 100;

    float x;
    float y;

    public GameObject areaFishing;
    
    void Start()
    {
        x = Screen.width / 2;
        y = Screen.height / 2;
        mFoodBar.minHunger = 0;
        mFoodBar.maxHunger = food;
        startFood = food;

        mText.SetActive(false);
        mText2.SetActive(true);        
    }

    public void StartInvoking() {
        InvokeRepeating("IncreaserHunger", 0, rateHunger);
    }


    public void IncreaserHunger()
    {
        food--;
        if(food < 0)
        {
            food = 0;
        }

        mFoodBar.SetValue(food);

        if (food == 0) {
            //playerStatus.SetDeadByHunger(true);
            CycleJourNuit.StopAllPlayerEvents();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Death", transform.position);
            GetComponent<template>().Famine();
        }

        if (playerStatus.GetDeadByHunger() || playerStatus.GetIsRestingStatus())
        {
            CancelInvoke();
        }
    }

    public bool IsDead
    {
        get
        {
            return food == 0;
        }
    }

    public void Eat(int amount)
    {
        food += amount;
        if(food >startFood)
        {
            food = startFood;
        }

        mFoodBar.SetValue(food);
    }

    public void Ray3D() {
        if (_selection != null)
        {
            mText.SetActive(false);
            mText2.SetActive(false);
            _selection = null;
        }
        var ray = player_camera.ScreenPointToRay(new Vector2(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5, ~(1 << 11)))
        {
            /*var selection = hit.transform;
            if (selection.CompareTag(foodTag)) {
                ///var selectionRenderer = selection.GetComponent<Renderer>();
                var selectionEat = selection.GetComponent<Food>();
                if (Input.GetMouseButtonDown(1)) {
                    Eat(selectionEat.m_food.eatInfo);
                    Destroy(selection.gameObject);
                }*/
            var selection = hit.transform;
            if (selection.CompareTag(interactiveTag) && areaFishing.GetComponent<FishingScript>().beingCatch == false)
            {
                var itemInteraction = selection.GetComponent<ItemInteraction>();
                mText2.SetActive(true);
                if (itemInteraction.GetCollectible() != null) {
                    mTextName2.text = itemInteraction.GetCollectible()._name;
                }
                else {
                    mTextName2.text = itemInteraction.GetName();
                }

                if (itemInteraction.notInventory == false)
                {
                    mTextInteraction2.text =  GameManager.GM.interaction + "\n Pour mettre dans l'inventaire";
                }
                else
                {
                    mTextInteraction2.text =  GameManager.GM.interaction + "\n Pour utiliser";
                }

            }
            else if (selection.CompareTag(foodTag))
            {
                mText.SetActive(true);
                if (selection.GetComponent<ItemInteraction>().GetCollectible() != null) {
                    mTextName.text = selection.GetComponent<ItemInteraction>().GetCollectible()._name;
                }
                else {
                    mTextName.text = selection.GetComponent<ItemInteraction>().GetName();
                }
                mTextFood.text = GameManager.GM.eating + "\n Pour manger";
                mTextInteraction.text =  GameManager.GM.interaction + "\n Pour mettre dans l'inventaire";
                var selectionRenderer = selection.GetComponent<Renderer>();
                var selectionEat = selection.GetComponent<Food>();
                /*if(selectionRenderer != null)
                {*/
                    if(Input.GetKeyDown(GameManager.GM.eating))
                    {
                        Eat(selectionEat.m_food.eatInfo);
                        Destroy(selection.gameObject);
                    }
                    /*else if (Input.GetKeyDown("Eating")) {
                        Eat(selectionEat.m_food.eatInfo);
                        Destroy(selection.gameObject);
                    }*/
                /*}*/
            }
            else
            {
                _selection = selection;
                mText.SetActive(false);
                mText2.SetActive(false);
            }
        }
        else
        {
            mText.SetActive(false);
            mText2.SetActive(false);
        }
    }

    private void Update()
    {
        Ray3D();
    }
}
