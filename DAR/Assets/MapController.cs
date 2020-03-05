using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public RectTransform map;
    private bool mapOpen;
    public GameObject iconMap;
    public GameObject markerIcon;
    private int markerCount;
    public RectTransform treasureIcon;
    public bool mapComplete;
    private bool m1_isAxisInUse = false;
    private bool m2_isAxisInUse = false;

    [FMODUnity.EventRef]
    public string mapSoundEvent;
    FMOD.Studio.EventInstance mapSound;

    [FMODUnity.EventRef]
    public string iconSoundEvent;
    FMOD.Studio.EventInstance iconSound;
    public Animator updateMapAnim;

    void Start()
    {
        mapOpen = map.GetComponent<Image>().enabled;
        markerCount = 0;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if(m1_isAxisInUse == false)
            {
                ShowAndHideMap();
                m1_isAxisInUse = true;
            }
        }
        if( Input.GetAxisRaw("Fire1") == 0)
        {
            m1_isAxisInUse = false;
        }

        if (Input.GetAxisRaw("Fire2") != 0)
        {
            if(m2_isAxisInUse == false && mapOpen == true && markerCount < 3)
            {
                MarkerOnMap();
                m2_isAxisInUse = true;
            }
        }
        if( Input.GetAxisRaw("Fire2") == 0)
        {
            m2_isAxisInUse = false;
        }

        int layerMask = 1 << 12;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20f, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            CityIcon cityOnSight = hit.transform.GetComponent<CityIcon>();
            if (!cityOnSight.discovered)
            {
                MapUpdate(cityOnSight.transform.position, cityOnSight.emblem);
                cityOnSight.discovered = true;
            }
            
        }
    }
    
    void ShowAndHideMap()
    {
        //map.GetComponent<Image>().enabled = !map.GetComponent<Image>().enabled;
        Image[] icons = map.GetComponentsInChildren<Image>();
        foreach( Image icon in icons )
        {
            icon.enabled = !icon.enabled ;
        }
        mapOpen = map.GetComponent<Image>().enabled;
        mapSound = FMODUnity.RuntimeManager.CreateInstance(mapSoundEvent);
        mapSound.start();
    }

    void MapUpdate(Vector3 newPosition, Sprite pointer)
    {
        //MAJ auto de la carte
        Vector3 TexturePosition = new Vector3((newPosition.x*7/5) + 150, newPosition.z + 150, 0);
        GameObject newIcon = Instantiate(iconMap, TexturePosition, Quaternion.identity, map);
        newIcon.GetComponent<RectTransform>().anchoredPosition = TexturePosition;
        newIcon.transform.up = treasureIcon.position - newIcon.transform.position;
        newIcon.GetComponent<Image>().sprite = pointer;
        newIcon.GetComponent<Image>().enabled = map.GetComponent<Image>().enabled;

        updateMapAnim.SetTrigger("AnimTrigger");
        
        iconSound = FMODUnity.RuntimeManager.CreateInstance(iconSoundEvent);
        iconSound.start();
    }

    void MarkerOnMap()
    {
        //Placement d'un marqueur de position du joueur
        Vector3 newPosition = transform.position;
        Vector3 TexturePosition = new Vector3((newPosition.x*7/5) + 150, newPosition.z + 150, 0);
        GameObject newMarker = Instantiate(markerIcon, TexturePosition, Quaternion.identity, map);
        newMarker.GetComponent<RectTransform>().anchoredPosition = TexturePosition;
        newMarker.GetComponent<Image>().enabled = map.GetComponent<Image>().enabled;
        markerCount++;
        
        updateMapAnim.SetTrigger("AnimTrigger");

        iconSound = FMODUnity.RuntimeManager.CreateInstance(iconSoundEvent);
        iconSound.start();
    }

    void PointIconToTreasure(RectTransform arrow)
    {
        //Assigner up
        
        //Vector3 relative = transform.InverseTransformPoint(treasureIcon.GetComponent<RectTransform>().anchoredPosition);
        //Vector3 relative = arrow.transform.position - treasureIcon.transform.position;
        
        //Vector3 dir = treasureIcon.GetComponent<RectTransform>().anchoredPosition - arrow.anchoredPosition;
        //float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        //arrow.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //arrow.transform.LookAt(dir);

        //Debug.Log ("dir : " + dir);
        //Debug.Log ("rotation : " + arrow.localRotation);

        //arrow.pivot = new Vector2(0, 0.5f);
        //float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        //arrow.localRotation = Quaternion.Euler(0, 0, angle);

        /*Vector3 differenceVector = arrow.transform.position - treasureIcon.transform.position;
 
        imageRectTransform.sizeDelta = new Vector2( differenceVector.magnitude, 1);
        imageRectTransform.pivot = new Vector2(0, 0.5f);
        imageRectTransform.position = treasureIcon.transform.position;
        float angle2 = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        imageRectTransform.localRotation = Quaternion.Euler(0,0, angle2);*/
    }
}
