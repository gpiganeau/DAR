using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptingTerrain : MonoBehaviour
{
    public Shader _drawShader;
    public GameObject _terrain;
    public Transform characterTransform;
    private float[,] startHeigthsMap;
    private Terrain myTerrain;
    private Material _snowMaterial, _drawMaterial;
    private RenderTexture _splatmap;
    [Range(1, 500)]
    public float _brushImpact;
    [Range(0, 1)]
    public float _brushStrength;


    RaycastHit _groundHit;
    int _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        myTerrain = this.gameObject.GetComponent<Terrain>();
        startHeigthsMap = myTerrain.terrainData.GetHeights(0, 0, 513, 513);
        _layerMask = LayerMask.GetMask("Ground");
        _drawMaterial = new Material(_drawShader);
        //_drawMaterial.SetVector("_Color", Color.red);

        _brushImpact = 10;
        _brushStrength = 0.3f;

        _terrain = this.gameObject;
        _snowMaterial = _terrain.GetComponent<MeshRenderer>().material;
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatmap);

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(characterTransform.position, Vector3.down, out _groundHit, 1f, _layerMask)) {
            if (_groundHit.collider.gameObject == this.gameObject) {
                _drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strength", _brushStrength);
                _drawMaterial.SetFloat("_Size", _brushImpact);
                RenderTexture tempTex = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, tempTex);
                Graphics.Blit(tempTex, _splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(tempTex);
            }
        }
    }
}
