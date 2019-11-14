using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_foot : MonoBehaviour
{
    ContactPoint[] cp_list;
    int _layerMask;
    int _layer;
    // Start is called before the first frame update
    void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
        _layer = 9; //ground
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] underSnowHits;
        Ray underSnowRay = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        underSnowHits = Physics.RaycastAll(underSnowRay, 0.3f);
        Debug.Log(underSnowHits.Length);
        foreach(RaycastHit hit in underSnowHits){
            Debug.Log(hit.collider.ToString());
            hit.collider.gameObject.GetComponent<FPS_Tracks>().HitReceived(hit);
        } 
    }
}
