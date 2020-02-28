using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowOnTerrainManager : MonoBehaviour
{
    //snowNoise
    public Shader _snowfallShader;
    private Material _snowFallMat;
    private MeshRenderer _meshRenderer;
    [Range(0.001f, 0.1f)]
    public float _flakeAmount;
    [Range(0f, 1f)]
    public float _flakeOpacity;


   

    // Start is called before the first frame update
    void Start()
    {
        //snowNoiseSetup
        _snowFallMat = new Material(_snowfallShader);
        _flakeAmount = 0.003f;
        _flakeOpacity = 0.1f;

        SnowNoise[] snowNoiseTiles = GetComponentsInChildren<SnowNoise>();
        foreach (SnowNoise snowNoiseTile in snowNoiseTiles) {
            snowNoiseTile.SetValues(_snowFallMat, _flakeAmount, _flakeOpacity);
        }


    }

    public void ResetTracks() {
        FPS_Tracks[] FPSTracksScripts = GetComponentsInChildren<FPS_Tracks>();
        foreach (FPS_Tracks FPSTracksScript in FPSTracksScripts) {
            FPSTracksScript.ResetTracks();
        }
    }

}
