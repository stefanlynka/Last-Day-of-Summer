using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Camera mainCamera;
    public GameObject slotCollider;
    public GameObject prevCollider;
    public bool wasDragging = false;
    public bool dragging = false;
    public bool overSlot = false;
    public bool flicker = false;


    // Start is called before the first frame update
    void Start(){
        mainCamera = GameObject.Find("/Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update(){

        if(StateController.State==0) UpdateSlots();

        UpdateLayer();
        KeepInSlot();
    }

    void UpdateSlots() {
        if (Input.GetMouseButtonUp(0) && overSlot && !slotCollider.GetComponent<Slot>().occupied) {
            dragging = false;
            PutInSlot(slotCollider);
        }
        else if (Input.GetMouseButtonUp(0)) {
            dragging = false;
            if (prevCollider) {
                PutInSlot(prevCollider);
            }
        }
    }

    void KeepInSlot() {
        if (prevCollider != null) {
            if (!Input.GetKey(KeyCode.Mouse0)) {
                Vector3 newPos = prevCollider.transform.position + new Vector3(0, 0, -0.5f);
                transform.position = newPos;
            }
            GetComponent<BoxCollider2D>().size = new Vector2(2.9f, GetComponent<BoxCollider2D>().size.y);
        }
    }

    void UpdateLayer() {
        if (!wasDragging && dragging) {
            MoveChildrenInLayer(this.gameObject, 10);
            wasDragging = true;
        }
        else if (wasDragging && !dragging) {
            MoveChildrenInLayer(this.gameObject, -10);
            wasDragging = false;
        }
    }

    void MoveChildrenInLayer(GameObject child, int offset) {
        SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.sortingOrder += offset;
        if (child.GetComponent<MeshRenderer>()!=null) child.GetComponent<MeshRenderer>().sortingOrder += offset;

        for (int i = 0; i < child.transform.childCount; i++) {
            GameObject nextChild = child.transform.GetChild(i).gameObject;
            MoveChildrenInLayer(nextChild, offset);
        }
    }

    private void OnMouseDrag() {
        if (StateController.State == 0 || StateController.State == 4) {
            dragging = true;
            TakeOutOfSlot();
            Vector3 newPos = Input.mousePosition;
            newPos.z = 20;
            transform.position = mainCamera.ScreenToWorldPoint(newPos);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Slot") {
            overSlot = true;
            slotCollider = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Slot") {
            overSlot = false;
        }
    }

    public void PutInSlot(GameObject slot) {
        Vector3 newPos = slot.transform.position + new Vector3(-0.25f, 0, 0);
        newPos.z = transform.position.z;
        transform.position = newPos;
        float scaleX = slot.transform.localScale.x;//GetComponent<Slot>().size;
        float scaleY = slot.transform.localScale.y;//GetComponent<Slot>().size;
        transform.localScale = new Vector2(scaleX / 10, scaleY / 10);

        slot.GetComponent<Slot>().occupied = true;
        slot.GetComponent<Slot>().Occupant = gameObject;
        prevCollider = slot;
        slotCollider = slot;
    }

    public void TakeOutOfSlot() {
        if (prevCollider != null) {
            prevCollider.GetComponent<Slot>().occupied = false;
            prevCollider.GetComponent<Slot>().Occupant = null;
        }
        
    }

}
