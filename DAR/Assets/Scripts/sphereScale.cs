using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereScale : MonoBehaviour
{
    public float x = 10f;
    public float y = 10f;
    public float z = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
           this.transform.localScale += new Vector3(x, y, z);
        }

        if (Input.GetKeyDown("e") && x >= 10 && y >= 10 && z >= 10)
        {
            this.transform.localScale -= new Vector3(x, y, z);
        }
    }
}
