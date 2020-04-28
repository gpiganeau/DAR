using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailOnTerrain : MonoBehaviour
{
    public GameObject playerObj = null;

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
    private Vector2Int[] brush;
    private Vector2Int lastPosition;
    [SerializeField] private int indexOfTrail;


    private struct ChangeArray {
        public int quantity;
        public Vector2Int[] coordinatesArray;
    }

    void Start()
    {
        if(playerObj == null) {
            playerObj = GameObject.Find("player");
        }
        terr = this.gameObject.GetComponent<Terrain>();
        td = terr.terrainData;
        hmWidth = td.alphamapWidth;
        hmHeight = td.alphamapHeight;
        mapResolution = td.alphamapResolution;
        playerHasPassed = new bool[mapResolution, mapResolution];
        lastPosition = FindPlayerPosition();
    }

    // Update is called once per frame
    void Update() {
        if (playerObj.GetComponent<PlayerMovement>().AmIGrounded()) {
            playerPositionOnTerrain = FindPlayerPosition();
            if (lastPosition != playerPositionOnTerrain) {
                brush = UpdateBrush(playerPositionOnTerrain, 2);
                ChangeArray tilesToChange = WhoChanged(brush);
                UpdateVisibleTrail(tilesToChange);
                lastPosition = playerPositionOnTerrain;
            }
        }
    }

    private ChangeArray WhoChanged(Vector2Int[] brush) {
        ChangeArray retChangeArray;
        retChangeArray.quantity = 0;
        retChangeArray.coordinatesArray = new Vector2Int[brush.Length];
        foreach(Vector2Int coordinate in brush) {
            if (!playerHasPassed[coordinate.x, coordinate.y]) {
                playerHasPassed[coordinate.x, coordinate.y] = true;
                retChangeArray.coordinatesArray[retChangeArray.quantity] = coordinate;
                retChangeArray.quantity += 1;
            }
        }
        Vector2Int[] temp = new Vector2Int[retChangeArray.quantity];
        Array.Copy(retChangeArray.coordinatesArray, temp, retChangeArray.quantity);
        retChangeArray.coordinatesArray = temp;
        return retChangeArray;
    }

    private Vector2Int[] UpdateBrush(Vector2Int position, int size, string type = "Trail") {
        brush = new Vector2Int[0];
        if (type == "Trail") {
            brush = new Vector2Int[size * size];
            int offset = Mathf.FloorToInt(size / 2);
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    brush[i * size + j] = new Vector2Int(playerPositionOnTerrain.x - offset + i, playerPositionOnTerrain.y - offset + j);
                }
            }
            return brush;
        }
        return brush;
    }

    private void UpdateVisibleTrail(ChangeArray tilesToChange) {
        if (tilesToChange.coordinatesArray.Length != 0) {
            int posX = Vector2Min("X", tilesToChange.coordinatesArray);
            int posY = Vector2Min("Y", tilesToChange.coordinatesArray);
            int length = Vector2Max("X", tilesToChange.coordinatesArray) - posX + 1;
            int width = Vector2Max("Y", tilesToChange.coordinatesArray) - posY + 1;
            float[,,] map = new float[width, length, td.alphamapLayers];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < length; j++) {
                    for(int k =0; k < td.alphamapLayers; k++) {
                        map[i, j, k] += (k == indexOfTrail) ? 1f : 0f;
                    }
                }
            }
            td.SetAlphamaps(posX, posY, map);
        }
    }

    private int Vector2Max(string v, Vector2Int[] coordinatesArray) {
        int max = -1;
        if (v == "X") {
            max = coordinatesArray[0].x;
            foreach (Vector2Int vect in coordinatesArray) {
                if (vect.x > max) {
                    max = vect.x;
                }
            }
        }
        else if (v == "Y") {
            max = coordinatesArray[0].y;
            foreach (Vector2Int vect in coordinatesArray) {
                if (vect.y > max) {
                    max = vect.y;
                }
            }
        }
        return max;
    }

    private int Vector2Min(string v, Vector2Int[] coordinatesArray) {
        int min = -1;
        if (v == "X") {
            min = coordinatesArray[0].x;
            foreach (Vector2Int vect in coordinatesArray) {
                if (vect.x < min) {
                    min = vect.x;
                }
            }
        }
        else if (v == "Y") {
            min = coordinatesArray[0].y;
            foreach (Vector2Int vect in coordinatesArray) {
                if (vect.y < min) {
                    min = vect.y;
                }
            }
        }
        return min;
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
