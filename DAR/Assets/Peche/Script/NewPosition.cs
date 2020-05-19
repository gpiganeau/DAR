using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPosition : MonoBehaviour
{
    public GameObject areaFishing;
    public GameObject areaFishing2;
    public GameObject rode;
    public GameObject thread;
    [SerializeField] private GameObject cameraDespo;
    public GameObject player;
    public bool isFishing;
    private bool mouseLookState;
    public Animator anim;
    public float speed = 0.5f;
    public float timerT = 2f;
    public bool toStop = false;

    
    void Start()
    {
        //areaFishing.SetActive(false);
        mouseLookState = cameraDespo.GetComponent<mouseLook>().ignore;     
    }

    void Update()
    {
        if (isFishing == true)
        {
            NewPositionPlayer();
        }

        else if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Restart");
            ItsTimeToStop();
        }

        if (toStop == true)
        {
            timerT -= Time.deltaTime;
            if (timerT < 0)
            {
                OldPositionPlayer();
            }
        }
    }

    public void NewPositionPlayer()
    {
        //Vector3 relativePos = areaFishing.transform.position - cameraDespo.transform.position;
        areaFishing.SetActive(true);
        areaFishing2.SetActive(false);
        rode.SetActive(true);
        cameraDespo.GetComponent<mouseLook>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<CameraController>().enabled = false;
        player.GetComponentInChildren<Headbobber>().enabled = false;
        //Quaternion rotationM = Quaternion.FromToRotation(cameraDespo.transform.up, relativePos);
        //cameraDespo.transform.rotation = Quaternion.Lerp(cameraDespo.transform.rotation,rotationM, speed*Time.deltaTime);
        cameraDespo.transform.LookAt(areaFishing.transform.position);
        isFishing = false;
    }

    public void ItsTimeToStop()
    {
        thread.SetActive(false);
        anim.SetBool("isStop",true);
        toStop = true;
    }

    public void OldPositionPlayer()
    {
        areaFishing.SetActive(false);
        areaFishing2.SetActive(true);
        rode.SetActive(false);
        cameraDespo.GetComponent<mouseLook>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<CameraController>().enabled = true;
        player.GetComponentInChildren<Headbobber>().enabled = true;
        anim.SetBool("isStop",false);
        timerT = 2f;
        toStop = false;

    }


}
