using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetInEnvironment : MonoBehaviour
{
    //item pose variables
    Item itemInHand;
    GameObject instanciatedItem;
    Transform hand;
    Transform originalHandPosition;
    [SerializeField] PlayerInventoryManager playerInventoryManager;
    [SerializeField] float rotSpeed;
    [SerializeField] HungerSystem m_Hunger;
    private bool m_isAxisInUse = false;
    public Camera player_camera;
    Vector2 center;
    float x;
    float y;

    Vector3 offset; //vector by which to move the transform so that it is all the way out of the ground

    public void Eat(Item item, int index) {
        m_Hunger.Eat(item.eatInfo);
        playerInventoryManager.RemoveItemAt(index);
    }

    public void PutInHand(Item item, int index) {
        if (!itemInHand) {
            instanciatedItem = Instantiate(item.prefab, hand);
            instanciatedItem.layer = 11;

            try {
                offset = new Vector3(0, instanciatedItem.GetComponent<MeshFilter>().mesh.bounds.extents.y * transform.localScale.y / 2, 0);
            }
            catch {
                offset = new Vector3(0, instanciatedItem.GetComponentInChildren<MeshFilter>().mesh.bounds.extents.y * instanciatedItem.GetComponentInChildren<MeshFilter>().transform.localScale.y / 2, 0);
            }
            Debug.Log(offset.ToString());
            itemInHand = item;
            playerInventoryManager.RemoveItemAt(index);
        }
        else {
            Destroy(instanciatedItem);
            playerInventoryManager.AddItem(itemInHand);
            instanciatedItem = Instantiate(item.prefab, hand);
            instanciatedItem.layer = 11;
            itemInHand = item;
            playerInventoryManager.RemoveItemAt(index);
        }
        playerInventoryManager.ShowUI();
    }

    public void HandToWorld() {
        if (itemInHand) {
            GameObject putDown = Instantiate(instanciatedItem, hand.position, hand.rotation);
            putDown.layer = 0;
            Destroy(instanciatedItem);
            instanciatedItem = null;
            itemInHand = null;
        }
    }

    private void Update() {
        hand.rotation.SetLookRotation(Vector3.up, new Vector3(0,1,0));
        if (itemInHand) {
            if (Input.GetButtonDown("Fire") && !playerInventoryManager.IsUIOpen()) {
                HandToWorld();
            }
            if (Input.GetButton("Recharge") && !playerInventoryManager.IsUIOpen()) {
                hand.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
            }
            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
            if (Physics.Raycast(ray, out hit, 10, ~(1 << 11))) {
                hand.position = hit.point + offset;
            }
            else {
                hand.localPosition = new Vector3(0, 0, 10);
                Physics.Raycast(hand.position, Vector3.down, out hit, 50f, ~(1 << 11));
                hand.position = hit.point + offset;
            }
        }
    }

    private void Start() {
        hand = transform.GetChild(0).GetChild(0);
        x = Screen.width / 2;
        y = Screen.height / 2;
    }
}
