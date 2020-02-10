using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will_level : MonoBehaviour
{
    [SerializeField] float current_level;
    [SerializeField] float max_level;
    [SerializeField] float increment;
    // Start is called before the first frame update
    void Start()
    {
        current_level = max_level;
    }

    // Update is called once per frame
    void Update()
    {
        current_level -= increment * Time.deltaTime;
    }



    public void ChangeByValue(float value) {
        current_level -= value;
    }


    //Setter functions
    public void SetIncrement(float value) {
        increment = value;
    }

    public void SetMaxLevel(float level) {
        max_level = level;
    }
}
