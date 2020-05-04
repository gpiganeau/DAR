using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HungerSystem : MonoBehaviour
{
    [SerializeField] private string foodTag = "Food";
    [SerializeField] private Material high;
    public Camera player_camera;


    public Hunger mFoodBar;
    private int startFood;

    public float rateHunger = 0.5f;
    public int food = 100;

    float x;
    float y;


    void Start()
    {
        x = Screen.width / 2;
        y = Screen.height / 2;
        mFoodBar.minHunger = 0;
        mFoodBar.maxHunger = food;
        startFood = food;

        InvokeRepeating("IncreaserHunger",0, rateHunger);
    }

    public void IncreaserHunger()
    {
        food--;
        if(food < 0)
        {
            food = 0;
        }

        mFoodBar.SetValue(food);

        if (IsDead)
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
        var ray = player_camera.ScreenPointToRay(new Vector2(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5, ~(1 << 11))) {
            var selection = hit.transform;
            if (selection.CompareTag(foodTag)) {
                ///var selectionRenderer = selection.GetComponent<Renderer>();
                var selectionEat = selection.GetComponent<Food>();
                if (Input.GetMouseButtonDown(1)) {
                    Eat(selectionEat.m_food.eatInfo);
                    Destroy(selection.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        Ray3D();
    }
}
