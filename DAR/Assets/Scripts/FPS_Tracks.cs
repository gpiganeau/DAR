using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Tracks : MonoBehaviour {
    private Material _snowMaterial, _drawMaterial;
    private RenderTexture _splatmap;
    public Shader _drawShader;
    public GameObject _terrain;
    public float rayCastDistance;
    public Transform[] characterTransforms;
    [Range(1, 500)]
    public float _brushImpact;
    [Range(0, 1)]
    public float _brushStrength;

    RaycastHit _groundHit;
    int _layerMask;

    // Start is called before the first frame update
    void Start() {
        _layerMask = LayerMask.GetMask("Ground");
        _drawMaterial = new Material(_drawShader);
        //_drawMaterial.SetVector("_Color", Color.red);

        _brushImpact = 500;
        _brushStrength = 1f;
        rayCastDistance = 0.05f;

        _terrain = this.gameObject;
        _snowMaterial = _terrain.GetComponent<MeshRenderer>().material;
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatmap);

    }

    public void HitReceived(RaycastHit _groundHit) {
        if (_groundHit.collider.gameObject == this.gameObject) {
            Debug.Log("in it for the money");
            _drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
            _drawMaterial.SetFloat("_Strength", _brushStrength);
            _drawMaterial.SetFloat("_Size", _brushImpact);
            RenderTexture tempTex = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_splatmap, tempTex);
            Graphics.Blit(tempTex, _splatmap, _drawMaterial);
            RenderTexture.ReleaseTemporary(tempTex);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
