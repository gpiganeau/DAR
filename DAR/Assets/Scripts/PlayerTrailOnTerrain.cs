using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailOnTerrain : MonoBehaviour
{
    private Vector2Int playerPositionOnTerrain;
    private Terrain terr; //the terrain on which the script is
    private int hmWidth; // heightmap width
    private int hmHeight; // heightmap height
    private int mapResolution; //heightmap resolution
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
        terr = this.gameObject.GetComponent<Terrain>();
        td = terr.terrainData;
        hmWidth = td.heightmapWidth;
        hmHeight = td.heightmapHeight;
        mapResolution = td.heightmapResolution;
        playerHasPassed = new bool[mapResolution, mapResolution];
    }

    // Update is called once per frame
    void Update()
    {
        playerPositionOnTerrain = FindPlayerPosition();
        Debug.Log(playerPositionOnTerrain.ToString());
        ModifyPassedArray(playerPositionOnTerrain);
    }

    private void UpdateVisibleTrail(int x, int y) {
        int brushSize = 4;
        int offset = brushSize / 2;
        float[,,] map = new float[brushSize, brushSize, 2];
        for (int i = 0; i < brushSize; i++) {
            for (int j = 0; j < brushSize; j++) {
                map[i, j, 0] = 1f;
                map[i, j, 1] = 0f;
            }
        }
        td.SetAlphamaps(x - offset, y - offset, map);
    }

    private void ModifyPassedArray(Vector2Int playerPositionOnTerrain) {
        if (!playerHasPassed[playerPositionOnTerrain.x, playerPositionOnTerrain.y]) {
            if(playerObj.GetComponent<PlayerMovement>().AmIGrounded()){
                playerHasPassed[playerPositionOnTerrain.x, playerPositionOnTerrain.y] = true;
                UpdateVisibleTrail(playerPositionOnTerrain.x, playerPositionOnTerrain.y);
            }
        }
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
