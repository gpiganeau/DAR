using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTerrainOnDestroy : MonoBehaviour {
    #region Fields

    public Terrain Terrain;

    private float[,] originalHeights;
    private float[,,] originalTextureMap;

    #endregion

    #region Methods

    private void OnDestroy() {
        Terrain.terrainData.SetHeights(0, 0, originalHeights);
        Terrain.terrainData.SetAlphamaps(0, 0, originalTextureMap);
    }

    private void Start() {
        originalHeights = Terrain.terrainData.GetHeights(
            0, 0, Terrain.terrainData.heightmapWidth, Terrain.terrainData.heightmapHeight);
        originalTextureMap = Terrain.terrainData.GetAlphamaps(
            0, 0, Terrain.terrainData.heightmapResolution - 1, Terrain.terrainData.heightmapResolution - 1);
    }

    #endregion
}