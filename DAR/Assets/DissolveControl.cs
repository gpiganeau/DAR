using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveControl : MonoBehaviour
{
    public Renderer meshRenderer;
    public Material instancedMaterial;
    public float dissolveTimer = 1f;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<Renderer>();
        instancedMaterial = meshRenderer.material;
        instancedMaterial.SetFloat("_DissolveAmount", 1);
    }

    void Update()
    {
        instancedMaterial.SetFloat("_DissolveAmount", dissolveTimer);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(Dissolve()); 
        }
    }

    IEnumerator Dissolve()
    {
        for (int i = 0; i < 5000; i++)
        {
            if (dissolveTimer > 0f)
            {
                dissolveTimer -= 0.0002f;
                yield return null;
            }

        }
    }
}