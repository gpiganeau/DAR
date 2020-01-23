using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twinkleTwinkleLittleStar : MonoBehaviour
{
    Color hue;
    Light myLight;
    float valueValue;
    bool inCoroutine;
    //[SerializeField] float darkTime;
    //[SerializeField] float brightTime;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] float minStartTime;
    [SerializeField] float maxStartTime;
    [SerializeField] float randomTime;
    [SerializeField] float startTime;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        StartCoroutine(startingLights());   
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator startingLights()
    {

        startTime = Random.Range(minStartTime, maxStartTime);
        yield return new WaitForSeconds(startTime);
        myLight.enabled = true;
        //StartCoroutine(Blink());

    }


    IEnumerator Blink()
    {

        randomTime = Random.Range(minTime, maxTime);

        while (true)
        {
            if (myLight.enabled)
            {

                yield return new WaitForSeconds(randomTime);
                myLight.enabled = false;
            }

            else if (!myLight.enabled)
            {
                yield return new WaitForSeconds(randomTime / 5f);
                myLight.enabled = true;
            }

           

        }
    }


}

