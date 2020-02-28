using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceSpawnVolume : MonoBehaviour
{
    float xSize;
    float ySize;
    float zSize;

    [SerializeField] GameObject ressourcePrefab;
    // Start is called before the first frame update
    void Start()
    {
        xSize = transform.localScale.x;
        ySize = transform.localScale.y;
        zSize = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRessource() {
        Random.InitState(System.DateTime.Now.Millisecond);
        Vector3 playerPosition;
        float x = Random.Range(0, xSize);
        float z = Random.Range(0, zSize);

        playerPosition = this.transform.position;
        Vector3 Position = new Vector3(x, ySize, z);

        /*while ((playerPosition - TreasurePosition).magnitude < 100) {
            x = Random.value;
            z = Random.value;
            TreasurePosition = new Vector3(x * 300, 1, z * 210 - 105);
            TexturePosition = new Vector3(x * 653 - 326, z * 430 - 215, 0);
        }*/
        GameObject newTreasure = Instantiate(ressourcePrefab, Position, new Quaternion(0, 0, 0, 0));
        StickToSurface(newTreasure.transform);
    }

    private void StickToSurface(Transform item) {
        int layerMask = 1 << 9;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(item.position, item.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask)) {
            //Debug.DrawRay(item.position, item.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("Ground found");
            float distanceToGround = hit.distance;
            Vector3 currentPos = item.position;
            float newY = currentPos.y - distanceToGround;
            item.position = new Vector3(currentPos.x, newY, currentPos.z);
        }
        else {
            Debug.DrawRay(item.position, item.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Ground not found");
        }
    }
}
