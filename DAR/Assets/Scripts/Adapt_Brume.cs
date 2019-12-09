using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapt_Brume : MonoBehaviour
{
    public GameObject PlayerCharacter;
    private float height;
    private float lerpValue;
    private float brumeValue;

    public float minBrumeValue;
    public float maxBrumeValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        height = PlayerCharacter.transform.position.y;
        lerpValue = 1- (height / 100);
        brumeValue = Mathf.Lerp(minBrumeValue, maxBrumeValue, lerpValue);

        gameObject.GetComponent<Aura2API.AuraCamera>().frustumSettings.baseSettings.density = brumeValue;
    }
}
