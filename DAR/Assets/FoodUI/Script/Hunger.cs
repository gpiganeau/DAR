using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    public Image hungerBarEmpty;
    public Text hungerText;

    public float minHunger;
    public float maxHunger;

    private float mCurrentValue;
    private float mCurrentPercent;

    public void SetValue(float hunger)
    {
        if (hunger != mCurrentValue)
        {
            if(maxHunger - minHunger == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = hunger;
                mCurrentPercent = (float)mCurrentValue / (float)(maxHunger-minHunger);
            }
        }

        hungerText.text = string.Format("{0} %",Mathf.RoundToInt(mCurrentPercent*100));
        hungerBarEmpty.fillAmount = mCurrentPercent;
    }

    public float CurrentPercent
    {
        get{return mCurrentPercent;}
    }

        public float CurrentValue
    {
        get{return mCurrentValue;}
    }

    void Start()
    {
        SetValue(100);
    }
}
