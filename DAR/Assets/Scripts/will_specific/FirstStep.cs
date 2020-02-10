using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    KeyCode keyCode;
    int sign;

    float timeOfLastPress;

    public bool IHaveTapped { get; private set; }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode)) {
            IHaveTapped = true;

            float now = Time.time;
            float since = now - timeOfLastPress;
            timeOfLastPress = now;

            if (since > 0) {
                // one-over-time for a very non-linear payoff for rapid tapping
                float motion = 1.0f / since;

                // and lets make it even more non-linear by squaring it!
                motion *= motion;

            }
        }
    }
}
