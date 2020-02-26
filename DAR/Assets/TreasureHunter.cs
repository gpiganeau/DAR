using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunter : MonoBehaviour
{
    public int numberOfTreasures;
    //private Material _drawMaterial;
    //private Texture _map;
    //public Shader _drawShader;
    public int MaxNumberOfTreasures;
    public int TerrainSize;
    public GameObject TreasurePrefab;
    //public GameObject Pointer;


    // Start is called before the first frame update
    void Start()
    {
        numberOfTreasures = 0;
        if (MaxNumberOfTreasures == 0)
            MaxNumberOfTreasures = 1;
        if(TerrainSize == 0)
            TerrainSize = 650;
    }

    // Update is called once per frame
    void Update()
    {
        while(numberOfTreasures < MaxNumberOfTreasures) {
            SpawnTreasure();
        }

        
    }

    private void SpawnTreasure() {
        Vector3 playerPosition;
        float x = Random.value;
        float z = Random.value;

        playerPosition = this.transform.position;
        Vector3 TreasurePosition = new Vector3(x * 300, 1, z * 210 - 105);
        Vector3 TexturePosition = new Vector3(x * 653 - 326, z * 430 - 215, 0);

        while ((playerPosition - TreasurePosition).magnitude < 100) {
            x = Random.value;
            z = Random.value;
            TreasurePosition = new Vector3(x * 300, 1, z * 210 - 105);
            TexturePosition = new Vector3(x * 653 - 326, z * 430 - 215, 0);
        }
        Instantiate(TreasurePrefab, TreasurePosition, new Quaternion(0,0,0,0));
        numberOfTreasures += 1;
        //PutOnMap(TexturePosition);
    }

    /*private void PutOnMap(Vector3 TexturePosition) {
        Vector3 spot = new Vector3(TexturePosition.x, TexturePosition.y + 15, TexturePosition.z);
        Debug.Log(spot.ToString());
        Pointer.GetComponent<RectTransform>().anchoredPosition = spot;
    }*/

    void OnCollisionEnter(Collider other)
    {

    }

    public void TreasureFound(GameObject Treasure) {
        numberOfTreasures -= 1;
        Destroy(Treasure);
    }
}
