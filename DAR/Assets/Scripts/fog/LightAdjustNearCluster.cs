using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjustNearCluster : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Transform parentTransform;

    private Coroutine coroutine;

    private float densityValue;
    private float colliderSizeX;
    private float colliderSizeZ;
    private Vector3 colliderCenter;
    private float xCoord;
    private float zCoord;
    private float xprime;
    private float zprime;
    private float xLerpValue;
    private float zLerpValue;
    private float phi;
    private float Distance; //selon la norme deux de R²
    // private float shadowsValue;
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

    IEnumerator RestartLight() {
        while(directionalLight.intensity != 1) {
            directionalLight.shadowStrength = Mathf.MoveTowards(directionalLight.shadowStrength, .75f, Time.deltaTime);
            directionalLight.intensity = Mathf.MoveTowards(directionalLight.intensity, 1f, Time.deltaTime);

            yield return null;
        }
        
    }

    private void OnTriggerStay(Collider other) {
        xCoord = other.transform.position.x - colliderCenter.x;
        zCoord = other.transform.position.z - colliderCenter.z;


        phi = transform.rotation.eulerAngles.y;

        xprime = xCoord * Mathf.Cos(phi) + zCoord * Mathf.Sin(phi);
        zprime = - xCoord * Mathf.Sin(phi) + zCoord * Mathf.Cos(phi);
        //Distance = Mathf.Sqrt(   .Max(Mathf.Abs(xDistance), Mathf.Abs(yDistance));

        

        xprime = 2 * Mathf.Abs(xprime) / colliderSizeX;
        zprime = 2 * Mathf.Abs(zprime) / colliderSizeZ;

        Debug.Log(xprime);

        xLerpValue = Mathf.Clamp01(xprime - 0.5f);
        zLerpValue = Mathf.Clamp01(zprime - 0.5f);

        //Debug.Log(Mathf.Max(xLerpValue, zLerpValue) * 2);

        directionalLight.shadowStrength = Mathf.Lerp(.1f, .75f, Mathf.Max(xLerpValue, zLerpValue) * 2);
        directionalLight.intensity = Mathf.Lerp(.4f, 1, Mathf.Max(xLerpValue, zLerpValue) * 2);
    }

    private void OnTriggerExit(Collider other) {
        coroutine = StartCoroutine(RestartLight());
    }

    private void OnTriggerEnter(Collider other) {
        StopCoroutine(coroutine);
    }

}
    

