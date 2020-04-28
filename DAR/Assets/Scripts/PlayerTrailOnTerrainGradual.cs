using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailOnTerrainGradual : MonoBehaviour
{
    public GameObject playerObj = null;

    private Vector2Int playerPositionOnTerrain;
    private Terrain terr; //the terrain on which the script is
    private int hmWidth; // heightmap width
    private int hmHeight; // heightmap height
    private int mapResolution; //heightmap resolution
    private int posXInTerrain; // position of the game object in terrain width (x axis)
    private int posYInTerrain; // position of the game object in terrain height (z axis)
    private TerrainData td;
    private float[,,] splatmapData;
    private Vector2Int[] brush;
    [SerializeField] private int indexOfTrail;
    [SerializeField] private int[] indexOfUnstransformableLayers;
    private bool toBeChanged;


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
        
    }

    // Update is called once per frame
    void Update() {
        if (playerObj.GetComponent<PlayerMovement>().AmIGrounded()) {
            playerPositionOnTerrain = FindPlayerPosition();
            brush = UpdateBrush(playerPositionOnTerrain, 2);
            UpdateVisibleTrail(brush);
        }
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

    private void UpdateVisibleTrail(Vector2Int[] tilesToChange) {
        if (tilesToChange.Length != 0) {
            int posX = Vector2Min("X", tilesToChange);
            int posY = Vector2Min("Y", tilesToChange);
            int length = Vector2Max("X", tilesToChange) - posX + 1;
            int width = Vector2Max("Y", tilesToChange) - posY + 1;
            float[,,] map = new float[width, length, td.alphamapLayers];
            map = td.GetAlphamaps(posX, posY, length, width);
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < length; j++) {
                    toBeChanged = true;
                    for (int k = 0; k < td.alphamapLayers; k++) {
                        if(Array.Exists<int>(indexOfUnstransformableLayers, x => x == k)) {
                            toBeChanged = (map[i,j,k] < 0.5) && toBeChanged;
                        }
                    }
                    if (toBeChanged) {
                        for (int k = 0; k < td.alphamapLayers; k++) {
                            map[i, j, k] += (k == indexOfTrail) ? Time.deltaTime * 2 : -Time.deltaTime * 2;
                        }
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
