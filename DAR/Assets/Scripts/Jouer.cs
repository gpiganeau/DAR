using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jouer : MonoBehaviour
{
	Animator anim;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			anim.SetBool ("Angel", false);
			Debug.Log ("Désactivé");

		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			anim.SetBool ("Angel", true);
			Debug.Log ("Activé");


		}
    }
}
