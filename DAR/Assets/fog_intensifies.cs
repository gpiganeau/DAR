using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog_intensifies : MonoBehaviour
{
    public GameObject ExclusionSphere;

    private float densityValue;
    private float sizeValue;
    private float timer;
    public int TotalTime;
    public int HalfTime;
    public int Breather;
    // Start is called before the first frame update
    void Start(){
        Breather = -Breather;
        timer = Breather;
    }

    // Update is called once per frame
    void Update(){
        if (timer < HalfTime) {
            densityValue = Mathf.Lerp(-6.4f, -6.25f, timer / HalfTime);
            sizeValue = Mathf.Lerp(100, 50, timer / HalfTime);
        }
        else if (timer < TotalTime) {
            densityValue = Mathf.Lerp(-6.25f, -6.1f, (timer - HalfTime) / (TotalTime - HalfTime));
            sizeValue = Mathf.Lerp(50, 20, (timer - HalfTime) / (TotalTime - HalfTime));
        }
        else {
            densityValue = -6.1f;
            sizeValue = 20f;
        }
        ExclusionSphere.GetComponent<Aura2API.AuraVolume>().densityInjection.strength = densityValue;
        ExclusionSphere.transform.localScale = new Vector3(sizeValue, sizeValue, sizeValue);
        timer += Time.deltaTime;
    }

    public void Waypoint_Discovered() {
        timer = Breather;
        if (HalfTime > 30) {
            HalfTime -= 5;
            TotalTime -= 10;
            Breather -= 1;
        }
    }
}
