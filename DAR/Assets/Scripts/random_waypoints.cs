using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class random_waypoints : MonoBehaviour
{
    public int numberOfWaypoints;
    private Material _drawMaterial;
    private Texture _map;
    public Shader _drawShader;
    public int MaxNumberOfWaypoints;
    public int TerrainSize;
    public GameObject WaypointPrefab;
    public GameObject Pointer;


    // Start is called before the first frame update
    void Start()
    {
        numberOfWaypoints = 0;
        if (MaxNumberOfWaypoints == 0)
            MaxNumberOfWaypoints = 1;
        if(TerrainSize == 0)
            TerrainSize = 650;
    }

    // Update is called once per frame
    void Update()
    {
        while(numberOfWaypoints < MaxNumberOfWaypoints) {
            SpawnWaypoint();
        }
    }

    private void SpawnWaypoint() {
        Vector3 playerPosition;
        float x = Random.value;
        float z = Random.value;

        playerPosition = GameObject.Find("unitychan").gameObject.transform.position;
        Vector3 WaypointPosition = new Vector3(x * 630 - 315, 1, z * 630 - 315);
        Vector3 TexturePosition = new Vector3(x * 490 - 245, z * 490 - 245, 0); 

        while ((playerPosition - WaypointPosition).magnitude < 200) {
            x = Random.value;
            z = Random.value;
            WaypointPosition = new Vector3(x * 630 - 315, 1, z * 630 - 315);
            TexturePosition = new Vector3(x * 490 - 245, z * 490 - 245, 0);
        }
        Instantiate(WaypointPrefab, WaypointPosition, new Quaternion(0,0,0,0));
        numberOfWaypoints += 1;
        PutOnMap(TexturePosition);
    }

    private void PutOnMap(Vector3 TexturePosition) {
        Vector3 spot = new Vector3(TexturePosition.x, TexturePosition.y + 15, TexturePosition.z);
        Debug.Log(spot.ToString());
        Pointer.GetComponent<RectTransform>().anchoredPosition = spot;
    }

    public void Destroying(GameObject Waypoint) {
        numberOfWaypoints -= 1;
        Destroy(Waypoint);
    }

}
