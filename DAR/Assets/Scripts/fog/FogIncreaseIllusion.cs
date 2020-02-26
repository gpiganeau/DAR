using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogIncreaseIllusion : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Transform parentTransform;

    private float colliderSize;
    private Vector3 colliderCenter;
    private float densityValue;
    private float xDistance;
    private float zDistance;
    private float colliderSizeX;
    private float colliderSizeZ;
    private float LerpValue;
    private float Distance;
    // Start is called before the first frame update
    void Start()
    {
        densityValue = GetComponentInParent<Aura2API.AuraVolume>().densityInjection.strength;
        colliderSizeX = transform.localScale.x;
        colliderSizeZ = transform.localScale.z;
        colliderCenter = GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        xDistance = (other.transform.position.x - colliderCenter.x);
        zDistance = (other.transform.position.z - colliderCenter.z);
        Distance = Mathf.Sqrt(Mathf.Pow(xDistance,2) + Mathf.Pow(zDistance, 2));

        LerpValue = Mathf.Clamp01((Mathf.Abs(Distance) - (colliderSizeX / 4)) / (colliderSizeX / 2));
        Debug.Log(Distance);
        Debug.Log(Mathf.Clamp01((Mathf.Abs(Distance) - (colliderSizeX / 4)) / (colliderSizeX / 2)));


        GetComponentInParent<Aura2API.AuraVolume>().densityInjection.strength = Mathf.Lerp(1.4f, 0f, LerpValue*2);
        directionalLight.shadowStrength = Mathf.Lerp(.1f, .75f, LerpValue *2);
        directionalLight.intensity = Mathf.Lerp(.4f, 1, LerpValue * 2);
    }
}
