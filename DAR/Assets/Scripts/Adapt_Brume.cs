using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapt_Brume : MonoBehaviour
{
    public GameObject PlayerCharacter;
    private float height;
    private float lerpValue;
    private float brumeValue;
    private bool ignore;

    public float minBrumeValue;
    public float maxBrumeValue;


    // Start is called before the first frame update
    void Start()
    {
        ignore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ignore) {
            height = PlayerCharacter.transform.position.y;
            lerpValue = 1 - (height / 200);
            lerpValue = Mathf.Clamp(lerpValue, 0, 1);
            brumeValue = Mathf.Lerp(minBrumeValue, maxBrumeValue, lerpValue);

            gameObject.GetComponent<Aura2API.AuraCamera>().frustumSettings.baseSettings.density = brumeValue;
        }
        
    }

    public void OverrideBrumeValue(float value) {
        ignore = true;
        gameObject.GetComponent<Aura2API.AuraCamera>().frustumSettings.baseSettings.density = value;
    }

    public void StopOverride() {
        ignore = false;
    }

}
