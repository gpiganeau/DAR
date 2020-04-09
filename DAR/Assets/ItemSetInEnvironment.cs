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
    private bool m_isAxisInUse = false;
    public Camera player_camera;
    Vector2 center;
    float x;
    float y;


    public void PutInHand(Item item, int index) {
        if (!itemInHand) {
            instanciatedItem = Instantiate<GameObject>(item.prefab, hand);
            instanciatedItem.layer = 11;
            itemInHand = item;
            playerInventoryManager.RemoveItemAt(index);
        }
        else {
            Destroy(instanciatedItem);
            playerInventoryManager.AddItem(itemInHand);
            instanciatedItem = Instantiate<GameObject>(item.prefab, hand);
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
        if (Input.GetButtonDown("Fire") && !playerInventoryManager.IsUIOpen()) {
            HandToWorld();
        }
        if (Input.GetButton("Recharge") && !playerInventoryManager.IsUIOpen()) {
            hand.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        }
        RaycastHit hit;
        Ray ray = player_camera.ScreenPointToRay(new Vector2(x, y));
        if (Physics.Raycast(ray, out hit, 10, ~(1 << 11))) {
            hand.position = hit.point;
        }
        else {
            hand.localPosition = new Vector3(0, 0, 10);
            Physics.Raycast(hand.position, Vector3.down, out hit, 50f,~(1 << 11));
            hand.position = hit.point;
        }

    }

    private void Start() {
        hand = transform.GetChild(0).GetChild(0);
        x = Screen.width / 2;
        y = Screen.height / 2;
    }
}
