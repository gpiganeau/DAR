using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public Transform mainTransform;

    // Update is called once per frame
    void Update () {
       transform.RotateAround(mainTransform.position, Vector3.up, 5 * Time.deltaTime);
    }
}
