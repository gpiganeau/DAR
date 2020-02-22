using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjustNearCluster : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Transform parentTransform;
    private float densityValue;
    private float colliderSizeX;
    private float colliderSizeZ;
    private Vector3 colliderCenter;
    private float xDistance;
    private float zDistance;
    private float xLerpValue;
    private float zLerpValue;
    private float Distance; //selon la norme infinie de R²
    //private float shadowsValue;
    // Start is called before the first frame update
    void Start() {
        densityValue = GetComponentInParent<Aura2API.AuraVolume>().densityInjection.strength;
        colliderSizeX = parentTransform.localScale.x;
        colliderSizeZ = parentTransform.localScale.z;
        colliderCenter = GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerStay(Collider other) {
        xDistance = (other.transform.position.x - colliderCenter.x);
        zDistance = (other.transform.position.z - colliderCenter.z);
        //Distance = Mathf.Max(Mathf.Abs(xDistance), Mathf.Abs(yDistance));

        xLerpValue = Mathf.Clamp01((Mathf.Abs(xDistance) - (colliderSizeX / 4)) / (colliderSizeX / 2));
        zLerpValue = Mathf.Clamp01((Mathf.Abs(zDistance) - (colliderSizeZ / 4)) / (colliderSizeZ / 2));

        directionalLight.shadowStrength = Mathf.Lerp(.1f, .75f, Mathf.Max(xLerpValue, zLerpValue) * 2);
        directionalLight.intensity = Mathf.Lerp(.4f, 1, Mathf.Max(xLerpValue, zLerpValue) * 2);
    }
}
