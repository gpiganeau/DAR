using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailOnTerrain : MonoBehaviour
{
    private Vector2Int playerPositionOnTerrain;
    Terrain terr; //the terrain on which the script is
    int hmWidth; // heightmap width
    int hmHeight; // heightmap height

    private int posXInTerrain; // position of the game object in terrain width (x axis)
    private int posYInTerrain; // position of the game object in terrain height (z axis)
    private bool[,] playerHasPassed;
    private TerrainData td;
    private float[,,] splatmapData;
    private GameObject playerObj = null;

    // Start is called before the first frame update
    void Start()
    {
        if(playerObj == null) {
            playerObj = GameObject.Find("FPS_Character");
        }
        playerHasPassed = new bool[513, 513];
        Debug.Log(playerHasPassed[0,0]);
        terr = this.gameObject.GetComponent<Terrain>();
        td = terr.terrainData;
        hmWidth = td.heightmapWidth;
        hmHeight = td.heightmapHeight;
    }

    // Update is called once per frame
    void Update()
    {
        playerPositionOnTerrain = FindPlayerPosition();
        Debug.Log(playerPositionOnTerrain.ToString());
        ModifyPassedArray(playerPositionOnTerrain);
        UpdateVisibleTrail();
    }

    private void UpdateVisibleTrail() {
        float[,,] map = new float[512, 512, 2];
        for (int i = 0; i <= 511; i++) {
            for (int j = 0; j <= 511; j++) {
                if (playerHasPassed[i, j]) {
                    Debug.Log(i + j);
                    map[i, j, 0] = 1f;
                    map[i, j, 1] = 0f;
                }
                else {
                    map[i, j, 0] = 0f;
                    map[i, j, 1] = 1f;
                }
            }
        }
        td.SetAlphamaps(0, 0, map);
    }

    private void ModifyPassedArray(Vector2Int playerPositionOnTerrain) {
        playerHasPassed[playerPositionOnTerrain.x,playerPositionOnTerrain.y] = true;
    }

    private Vector2Int FindPlayerPosition() {
        // get the normalized position of this game object relative to the terrain
        
        Vector3 tempCoord = (playerObj.transform.position - terr.gameObject.transform.position);
        Vector3 coord;
        coord.x = tempCoord.x / terr.terrainData.size.x;
        coord.y = tempCoord.y / terr.terrainData.size.y;
        coord.z = tempCoord.z / terr.terrainData.size.z;

        // get the position of the terrain heightmap where this game object is
        posXInTerrain = (int)(coord.x * hmWidth);
        posYInTerrain = (int)(coord.z * hmHeight);
        return new Vector2Int(posXInTerrain, posYInTerrain);
    }

    private Vector2 WorldPosToTerrainXY(Terrain terrain, Vector3 wordCor) {
        Vector2 vecRet = new Vector2();
        Vector3 terPosition = terrain.transform.position;
        vecRet.x = ((wordCor.x - terPosition.x) / terrain.terrainData.size.x) * terrain.terrainData.heightmapWidth;
        vecRet.y = ((wordCor.z - terPosition.z) / terrain.terrainData.size.z) * terrain.terrainData.heightmapHeight;
        return vecRet;
    }
}
