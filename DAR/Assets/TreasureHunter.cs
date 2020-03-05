using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureHunter : MonoBehaviour
{
    public int numberOfTreasures;
    //private Material _drawMaterial;
    //private Texture _map;
    //public Shader _drawShader;
    public int MaxNumberOfTreasures;
    public Transform terrain;
    public Vector3 TerrainSize;
    public GameObject TreasurePrefab;
    public List<Transform> treasureList = new List<Transform>();
    //public Transform arrow;
    public GameObject Pointer;
    public bool endOfQuest;
    public GameObject lightOfTheEnd;
    [SerializeField] private GameObject resultPanel;


    // Start is called before the first frame update
    void Start()
    {
        if(TerrainSize == Vector3.zero)
            TerrainSize = terrain.GetComponent<Renderer>().bounds.size;
        endOfQuest = false;
        lightOfTheEnd.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        while(numberOfTreasures < MaxNumberOfTreasures && !endOfQuest) {
            SpawnTreasure();
        }

        /*if (treasureList.Count > 0)
        {
            arrow.LookAt(treasureList[0]);
        }*/

    }

    private void SpawnTreasure() {
        Random.InitState(System.DateTime.Now.Millisecond);
        Vector3 playerPosition;
        float x = Random.Range(0, 400.0f);
        float z = Random.Range(0, 400.0f);

        playerPosition = this.transform.position;
        Vector3 TreasurePosition = new Vector3(x, 20, z);
        Vector3 TexturePosition = new Vector3((x*7/5) + 150, z + 150, 0);

        /*while ((playerPosition - TreasurePosition).magnitude < 100) {
            x = Random.value;
            z = Random.value;
            TreasurePosition = new Vector3(x * 300, 1, z * 210 - 105);
            TexturePosition = new Vector3(x * 653 - 326, z * 430 - 215, 0);
        }*/
        GameObject newTreasure = Instantiate(TreasurePrefab, TreasurePosition, new Quaternion(0,0,0,0));
        StickToSurface(newTreasure.transform);
        treasureList.Add(newTreasure.transform);
        numberOfTreasures += 1;
        PutOnMap(TexturePosition);
    }

    private void StickToSurface(Transform item)
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(item.position, item.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(item.position, item.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("Ground found");
            float distanceToGround = hit.distance;
            Vector3 currentPos = item.position;
            float newY = currentPos.y - distanceToGround;
            item.position = new Vector3(currentPos.x, newY, currentPos.z);
        }
        else
        {
            Debug.DrawRay(item.position, item.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Ground not found");
        }
    }

    private void PutOnMap(Vector3 TexturePos) {
        Vector3 spot = TexturePos;
        //Debug.Log("Treasure Pos : " + spot.ToString());
        Pointer.GetComponent<RectTransform>().anchoredPosition = spot;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("collectible"))
        {
            TreasureFound(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other2)
    {
        if (other2.gameObject.CompareTag("endgame"))
        {
            PlayerWins();
        }
    }

    public void TreasureFound(GameObject Treasure) {
        numberOfTreasures -= 1;
        Destroy(Treasure);
        if (numberOfTreasures == 0)
        {
            endOfQuest = true;
            lightOfTheEnd.GetComponent<Renderer>().enabled = true;
            lightOfTheEnd.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void PlayerWins()
    {
        Time.timeScale = 0;
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<Text>().text = "You win !";
    }

    public void PlayerLoses()
    {
        Time.timeScale = 0;
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<Text>().text = "You lose...";
    }
}
