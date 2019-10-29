using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public Camera _camera;
    public Shader _drawShader;
    [Range(1,500)]
    public float _brushSize;
    [Range(0,1)]
    public float _brushStrength;


    private RenderTexture _splatmap;
    private Material _snowMaterial, _drawMaterial;
    private RaycastHit _hit;
    private Material _snowPrivateMaterial;
    private RenderTexture _splatmapModel;

        // Start is called before the first frame update
    void Start(){
        _drawMaterial = new Material(_drawShader);
        //_drawMaterial.SetVector("_Color", Color.red);

        _snowPrivateMaterial = GetComponent<MeshRenderer>().material;
        _snowMaterial = Material.Instantiate(_snowPrivateMaterial);

        _splatmapModel = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _splatmap = RenderTexture.Instantiate(_splatmapModel);
        //_splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatmap);
        _brushSize = 10;
        _brushStrength = 0.3f;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit)) {
                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strength", _brushStrength);
                _drawMaterial.SetFloat("_Size", _brushSize);
                RenderTexture tempTex = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, tempTex);
                Graphics.Blit(tempTex, _splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(tempTex);
            }
        }
        
    }
    private void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), _splatmap, ScaleMode.ScaleToFit, false, 1);
    }
}
