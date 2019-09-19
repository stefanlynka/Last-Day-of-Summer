using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissZone : MonoBehaviour
{
    public GameObject Character;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonUp(0) && Character && Character.GetComponent<Draggable>().dragging) {
            print("BLOW EM UP");
            Population.RemoveCharacter(Character);
            Destroy(Character);
        }
    }

    public void OnMouseUp() {
    }
    private void OnCollisionEnter2D(Collision2D collision) {

    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Character>() && Input.GetKey(KeyCode.Mouse0)) {
            Character = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Character>()) {
            Character = null;
        }
    }

}
