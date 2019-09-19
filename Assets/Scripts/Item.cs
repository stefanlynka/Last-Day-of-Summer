using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string itemName = "";

    private void Awake() {
    }

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        
    }

    public void SetupItem(string newText) {
        itemName = newText;
        SpriteRenderer rend = gameObject.AddComponent<SpriteRenderer>();
        GameObject textObject = Tools.MakeText(gameObject, itemName, new Vector3(0, 2, 0), 12, 20);
        rend.sprite = Resources.Load<Sprite>("Sprites/Items/" + itemName);
        print("child named " + Tools.GetChildNamed(gameObject, "Text"));
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<Draggable>();
        //Tools.GetChildNamed(gameObject, "Text").GetComponent<TextMesh>().text = newText;
    }

    private void OnMouseDown() {
        print("Clicked");
    }
}
