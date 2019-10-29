using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptingTerrain : MonoBehaviour
{
    private float[,] startHeigthsMap;
    // Start is called before the first frame update
    void Start()
    {
        startHeigthsMap = this.gameObject.GetComponent<Terrain>().terrainData.GetHeights(0, 0, 1, 1);
        Debug.Log(startHeigthsMap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
