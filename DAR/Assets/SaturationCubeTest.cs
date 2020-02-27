using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaturationCubeTest : MonoBehaviour
{

    public GameObject getCube;
    float h, s, v;

    // Start is called before the first frame update
    void Start()
    {
        getCube = GameObject.Find("Cube_Rouge");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           
            Color rgbColor = getCube.GetComponent<Renderer>().material.color;
            Color.RGBToHSV(rgbColor, out h,out s,out v);
            Debug.Log(h + " , " + s + " , " + v);

        }
    }
}
